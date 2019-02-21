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
                { "FindById", "UserRoles.UserId = @id" },
                { "InsertQuery",
                    "INSERT INTO UserRoles ([UserId],[Role]) " +
                    "VALUES (@UserId,@Role)" +
                    "SELECT * FROM UserRoles WHERE UserRoles.UserId = (SELECT SCOPE_IDENTITY()))"},
                { "RemoveQuery",
                    "DELETE FROM UserRoles WHERE UserId = @Id" },
                { "UpdateQuery",
                    "UPDATE UserRoles SET " +
                    "[UserId]=@UserId[Role]=@Role, [ImageCourt]=@ImageCourt, [MaxPlayers]=@MaxPlayers, [Price]=@Price " +
                    "WHERE Id = @Id"}
            };
            _db = projectDbContext;
        }

        public override IEnumerable<UserRoles> Get(string queryWhere = null, object parameters = null)
        {
            IEnumerable<UserRoles> userroles = null;

            _db.UsingConnection((dbCon) =>
            {
                userroles = dbCon.Query<UserRoles, User, UserRoles>(
                    "SELECT * FROM Useroles INNER JOIN User ON User.UserId = Branch.Id" + (queryWhere == null ? string.Empty : $" WHERE {queryWhere}"),
                    (userrole, user) =>
                    {
                        userrole.User = user;
                        return userrole;
                    },
                    splitOn: "id",
                    param: parameters)
                    .Distinct();
            });

            return userroles;
        }
    }
}
