using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
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

        public IEnumerable<User> GetAllUsers()
        {
            return _db.Users.Get();
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
            var role = _db.Roles.GetUserRole();

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
                .Get()
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

            IEnumerable<Branch> branches = _db.Branches
                .Nearest(latitudeFixed, longitudeFixed, FIXED_DISTANCE)
                .OrderBy(nb => nb.Distance);

            return new NearestBrachView()
            {
                Latitude = latitudeFixed,
                Longitude = longitudeFixed,
                Branches = branches
            };
        }
        #endregion
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

            return new MultiSelectList(_db.Facilities.All.OrderBy(f=>f.Description), "Id", "Description", branchSelectedFacilities);
        }
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
    }
}