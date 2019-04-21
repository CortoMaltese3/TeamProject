using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TeamProject.Models;
using Dapper;
using System.Security.Claims;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using TeamProject.Dal;

namespace TeamProject.Managers
{
    public class UserManager : TableManager<User>
    {
        public UserManager(ProjectDbContext projectDbContext) 
        {
            _db = projectDbContext;
        }

        public override IEnumerable<User> Get(string queryWhere = null, object parameters = null)
        {
            IEnumerable<User> courts = null;

            _db.UsingConnection((dbCon) =>
            {
                var userDictionary = new Dictionary<int, User>();
                courts = dbCon.Query<User, Role, User>(
                    "SELECT [User].*, Role.* FROM [User] " +
                    "LEFT JOIN UserRoles ON [User].Id = UserRoles.UserId " +
                    "LEFT JOIN Role ON UserRoles.RoleId = Role.Id " +
                    (queryWhere == null ? string.Empty : $" WHERE {queryWhere}"),
                    (user, role) =>
                    {
                        User userEntry;

                        if (!userDictionary.TryGetValue(user.Id, out userEntry))
                        {
                            userEntry = user;
                            userEntry.Roles = new List<Role>();
                            userDictionary.Add(userEntry.Id, userEntry);
                        }
                        if (role != null)
                        {
                            userEntry.Roles.Add(role);
                        }

                        return userEntry;
                    },
                        splitOn: "Id",
                        param: parameters)
                        .Distinct()
                        .ToList();
            });

            return courts;
        }

    }
}
