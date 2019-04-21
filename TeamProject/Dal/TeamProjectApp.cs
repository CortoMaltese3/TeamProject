using Dapper;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using TeamProject.Managers;
using TeamProject.Models;
using TeamProject.ModelsViews;

namespace TeamProject.Dal
{
    public class TeamProjectApp
    {

        private ProjectDbContext _db = new ProjectDbContext();
        #region Users 
        public User Login(string email, string password)
        {

            var loggedInUser =_db.Users.Get("Email=@email and Password=@password", new { email, password }).FirstOrDefault();

            if (loggedInUser != null)
            {
                var claims = new List<Claim>(new[]
                {
                    // adding following 2 claim just for supporting default antiforgery provider
                    new Claim(ClaimTypes.NameIdentifier, email),
                    new Claim(
                        "http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider",
                        "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"),
                    new Claim(ClaimTypes.Name, email),
                    new Claim(ClaimTypes.UserData, loggedInUser.Id.ToString())
                });

                foreach (var role in loggedInUser.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.Description));
                }

                var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

                HttpContext.Current.GetOwinContext().Authentication.SignIn(
                    new AuthenticationProperties { IsPersistent = false }, identity);
            }

            return loggedInUser;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _db.Users.All;
        }
        public bool EmailExists(string email)
        {
            return _db.Users.Get("Email=@email", new { email }).Count() > 0;
        }
        public (bool valid, string field, string error) AddUser(User user, bool isValid)
        {
            if (!isValid)
            {
                return (false, string.Empty, string.Empty);
            }
            if (EmailExists(user.Email))
            {
                return (false, "Email", "E-mail already taken! Choose an other E-mail!");
            }
            if (user.Password == null)
            {
                return (false, "Password", "Password is required");
            }
            if (!AddSimpleUser(user))
            {
                return (false, "UserName", "Failed to create new user.");
            }

            return (true, string.Empty, string.Empty);
        }
        private bool AddSimpleUser(User user)
        {
            //adding the new user in db!
            var newUser = _db.Users.Add(user);
            if (newUser == null)
            {
                return false;
            }

            //finding the role id for type "user"
            var role = GetUserRole();

            //adding the role id with user id in the connection table.
            var UserRole = new UserRoles()
            {
                UserId = newUser.Id,
                RoleId = role.Id
            };
            var newUserRoles = _db.UserRoles.Add(UserRole);

            return newUserRoles != null;
        }
        public bool GetUserById(int id, out User user)
        {
            user = _db.Users.Find(id);
            return user != null;
        }
        public void AddNotEnabledRolesToUser(User user)
        {
            // find roles not enabled for selected user
            var roles = _db
                .Roles
                .All
                .Where(r => !user.Roles.Any(ur => ur.Id == r.Id))
                .Select(r => new Role()
                {
                    Id = r.Id,
                    Description = r.Description,
                    IsEnabled = false,
                    IsNew = true
                });

            // add found roles to user
            user.Roles.AddRange(roles);
        }
        public bool UpdateUserAndRoles(User user)
        {
            // remove existing roles that got disabled 
            _db.UserRoles.Remove(user.Id);

            // add new roles
            user.Roles
                .Where(r => r.IsEnabled)
                .Select(r => new UserRoles() { UserId = user.Id, RoleId = r.Id })
                .ToList()
                .ForEach((role) =>
                {
                    _db.UserRoles.Add(role);
                });

            //update user
            return _db.Users.Update(user);
        }
        public void RemoveUserById(int id)
        {
            var user = _db.Users.Find(id);
            _db.Users?.Remove(id);
        }
        private Role GetUserRole()
        {
            return _db.Roles.Get("Description=@Description", new { Description = "User" }).FirstOrDefault();
        }
        #endregion
        #region Branches 
        private const double FIXED_DISTANCE = 20000;
        public NearestBrachView GetNearestBranches(string latitude, string longitude)
        {
            if (!double.TryParse(latitude, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out double latitudeFixed) ||
                !double.TryParse(longitude, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out double longitudeFixed))
            {

                return null;
            }

            IEnumerable<Branch> branches = Nearest(latitudeFixed, longitudeFixed, FIXED_DISTANCE)
                .OrderBy(nb => nb.Distance);

            return new NearestBrachView()
            {
                Latitude = latitudeFixed,
                Longitude = longitudeFixed,
                Branches = branches
            };
        }
        /// <summary>
        /// Returns List of branches near to a given latitude, longtitude
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longtitude"></param>
        /// <returns></returns>
        private IEnumerable<Branch> Nearest(double latitude, double longitude, double distanceInMeters = 5000)
        {
            IEnumerable<Branch> branches = Enumerable.Empty<Branch>();

            var branchDictionary = new Dictionary<int, Branch>();
            var facilitiyDictionary = new Dictionary<int, Facility>();
            _db.UsingConnection((dbCon) =>
            {
                branches = dbCon.Query<Branch, Court, Facility, Branch>(
                    "GetBranchesDistance",
                    (branch, court, facility) =>
                    {

                        if (!branchDictionary.TryGetValue(branch.Id, out Branch branchEntry))
                        {
                            branchEntry = branch;
                            branchEntry.Facility = new List<Facility>();
                            branchDictionary.Add(branchEntry.Id, branchEntry);
                        }

                        if (!branchEntry.Facility.Contains(facility))
                        {
                            branchEntry.Facility.Add(facility);
                        }

                        return branchEntry;
                    },
                    splitOn: "id",
                    param: new { Latitude = latitude, Longitude = longitude, Distance = distanceInMeters },
                    commandType: CommandType.StoredProcedure).Distinct();
            });
            return branches;
        }
        #endregion
        #region BranchFacilities
        /// <summary>
        /// returns a list of SelectListItem with branch selected facilities (with selected=true)
        /// plus available facilities from list
        /// </summary>
        /// <param name="branchId"></param>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetAvailableFacilities(int branchId)
        {

            var branchSelectedFacilities = _db.BranchFacilities
                .Get("BranchId = @branchId", new { branchId })
                .Select(f => f.FacilityId);
            
            return new MultiSelectList(_db.Facilities.All.OrderBy(f => f.Description), "Id", "Description", branchSelectedFacilities);
        }
        /// <summary>
        /// update selected facilities by first delete all records
        /// and then insert only selected ones
        /// </summary>
        /// <param name="facilityFormModel"></param>
        public void UpdateBranchFacilities(FacilityFormModel facilityFormModel)
        {
            // remove all branch facilities
            _db.BranchFacilities.Remove(facilityFormModel.BranchId);

            // add selected facilities
            if (facilityFormModel.SelectedFacilities != null)
            {
                foreach (var item in facilityFormModel.SelectedFacilities)
                {
                    _db.BranchFacilities.Add(new BranchFacilities() { BranchId = facilityFormModel.BranchId, FacilityId = item });
                }
            }
        }
        #endregion
        #region Bookings
        public IEnumerable<Booking> GetBranchBookings(int id, DateTime fromDate, DateTime toDate)
        {
            return _db.Bookings
                .Get("CourtId in (SELECT Court.Id FROM Court WHERE BranchId = @id) AND " +
                     "BookedAt Between @fromDate And @toDate", new
                     {
                         id,
                         fromDate,
                         toDate
                     });
        }

        public IEnumerable<Booking> GetCourtBookings(int id, DateTime fromDate, DateTime toDate)
        {
            return _db.Bookings
                .Get("CourtId=@CourtId AND BookedAt Between @fromDate And @toDate", new
                {
                    CourtId = id,
                    fromDate,
                    toDate
                });
        }
        #endregion

        #region Timeslots
        /// <summary>
        /// Gets Courts Timeslots availability by Hour for each week day (day 1..7)
        /// if a timeslot is booked then day 1..7 value should be equal to '2'
        /// else if it is available the value should be equal to '1'
        /// if there is no timeslot has been set for givel hour and day 1..7 the value should be null
        /// </summary>
        /// <param name="courtId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public IEnumerable<TimeslotApiView> GetForBooking(int courtId, DateTime fromDate, DateTime toDate)
        {
            return GetForTimeSlotsPivot(courtId, fromDate, toDate, "GetTimeslotsAt");
        }

        /// <summary>
        /// Gets Courts Timeslots set by Hour for each week day (day 1..7)
        /// day 1..7 should have the null value if no timeslot has been set
        /// or the Id number of the Timeshot record
        /// </summary>
        /// <param name="courtId"></param>
        /// <returns></returns>
        public IEnumerable<TimeslotApiView> GetForView(int courtId)
        {
            IEnumerable<TimeslotApiView> courtTimeslots = Enumerable.Empty<TimeslotApiView>();

            _db.UsingConnection((dbCon) =>
            {
                courtTimeslots = dbCon
                    .Query<TimeslotApiView>("GetTimeslots",
                        new
                        {
                            CourtId = courtId
                        },
                        commandType: CommandType.StoredProcedure);
            });

            return courtTimeslots;
        }

        /// <summary>
        /// Gets Courts Bookings by Hour for each week day (day 1..7)
        /// Day 1..7 value should be null if no timeslot is set at given hour/day
        /// or zero (0) if a timeslot is set but there is no booking 
        /// and finally equal to the Id of the Booking record 
        /// </summary>
        /// <param name="courtId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public IEnumerable<TimeslotApiView> GetBookings(int courtId, DateTime fromDate, DateTime toDate)
        {
            return GetForTimeSlotsPivot(courtId, fromDate, toDate, "GetBookingsAt");
        }

        /// <summary>
        /// Gets a list of TimeslotApiView for a courtId and a given date period
        /// using procedure from the parameters
        /// </summary>
        /// <param name="courtId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="procedure"></param>
        /// <returns></returns>
        private IEnumerable<TimeslotApiView> GetForTimeSlotsPivot(int courtId, DateTime fromDate, DateTime toDate, string procedure)
        {
            IEnumerable<TimeslotApiView> courtTimeslots = Enumerable.Empty<TimeslotApiView>();

            _db.UsingConnection((dbCon) =>
            {
                courtTimeslots = dbCon
                    .Query<TimeslotApiView>(procedure,
                        new
                        {
                            CourtId = courtId,
                            DateFrom = fromDate.ToString("yyyy-MM-dd"),
                            DateTo = toDate.ToString("yyyy-MM-dd 23:59:00.000")
                        },
                        commandType: CommandType.StoredProcedure);
            });

            return courtTimeslots;
        }

        #endregion
        public IEnumerable<Court> AllCourtsSameBranch(int courtId)
        {
            var branchId = _db.Courts.Find(courtId)?.BranchId ?? 0;
            return BranchCourts(branchId);
        }
        public IEnumerable<Court> BranchCourts(int branchId)
        {
            return _db.Courts.Get("branchId=@branchId", new { branchId });
        }


    }
}