using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
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
        public bool AddSimpleUser(User user)
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
            user.Roles
                .Where(r => !r.IsNew && !r.IsEnabled)
                .ToList()
                .ForEach((role) =>
                {
                    _db.UserRoles.Remove(user.Id, role.Id);
                });

            // add new roles
            user.Roles
                .Where(r => r.IsNew && r.IsEnabled)
                .ToList()
                .ForEach((role) =>
                {
                    _db.UserRoles.Add(new UserRoles() { UserId = user.Id, RoleId = role.Id });
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

            BranchManager branchManager = new BranchManager(new ProjectDbContext());
            IEnumerable<Branch> branches = _db.Branches.Nearest(latitudeFixed, longitudeFixed, FIXED_DISTANCE);

            return new NearestBrachView()
            {
                Latitude = latitudeFixed,
                Longitude = longitudeFixed,
                Branches = branches
            };
        }
        #endregion
    }
}