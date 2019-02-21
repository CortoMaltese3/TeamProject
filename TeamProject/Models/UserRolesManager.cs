using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;

namespace TeamProject.Models
{
    public class UserRolesManager : TableManager<UserRoles>
    {
        public UserRolesManager(ProjectDbContext projectDbContext)
        {
            _queryParts = new Dictionary<string, string>()
            {
                { "FindById", "UserRoles.Id = @id" },
                { "InsertQuery",
                    "INSERT INTO UserRoles ([UserId], [Role]) " +
                    "VALUES (@UserId, @Role)" +
                    "SELECT * FROM UserRoles WHERE UserRoles.Id = (SELECT SCOPE_IDENTITY()))"},
                { "RemoveQuery",
                    "DELETE FROM UserRoles WHERE Id = @Id" },
                { "UpdateQuery",
                    "UPDATE UserRoles SET " +
                    "[UserId]=@UserId, [Role]=@Role " +
                    "WHERE Id = @Id"}
            };
            _db = projectDbContext;
        }

        public override IEnumerable<UserRoles> Get(string queryWhere = null, object parameters = null)
        {
            IEnumerable<UserRoles> userRoles = null;

            _db.UsingConnection((dbCon) =>
            {
                userRoles = dbCon.Query<UserRoles, User, UserRoles>(
                    "SELECT * FROM Useroles INNER JOIN User ON Useroles.UserId = [User].Id " + (queryWhere == null ? string.Empty : $" WHERE {queryWhere}"),
                    (userRole, user) =>
                    {
                        userRole.User = user;
                        return userRole;
                    },
                    splitOn: "id",
                    param: parameters)
                    .Distinct();
            });

            return userRoles;
        }
    }
}
