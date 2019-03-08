using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TeamProject.Models;
using Dapper;
using System.Security.Claims;

namespace TeamProject.Models
{
    public class UserManager : TableManager<User>
    {
        public UserManager(ProjectDbContext projectDbContext)
        {
            _queryParts = new Dictionary<string, string>()
            {
                { "FindById", "[User].Id = @id" },
                { "InsertQuery",
                    "INSERT INTO [User] ([Firstname], [Lastname], [Email], [Password]) " +
                    "VALUES (@Firstname, @Lastname, @Email, @Password) " +
                    "SELECT * FROM [User] WHERE [User].Id = (SELECT SCOPE_IDENTITY())"},
                { "RemoveQuery",
                    "DELETE FROM [User] WHERE Id = @Id" },
                { "UpdateQuery",
                    "UPDATE [User] SET " +
                    "[Firstname]=@Firstname, [Lastname]=@Lastname, [Email]=@Email, [Password]=@Password " +
                    "WHERE Id = @Id"},
                //Login Query added.
                {"Login", "SELECT * FROM [User] WHERE Username=@Username and Password=@Password"}
            };
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
