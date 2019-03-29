using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TeamProject.Dal;
using TeamProject.Models;

namespace TeamProject.Managers
{
    public class RoleManager : TableManager<Role>
    {

        public RoleManager(ProjectDbContext projectDbContext)
        {
            _queryParts = new Dictionary<string, string>()
            {
                { "FindById", "Role.id = @id" },
                { "InsertQuery",
                    "INSERT INTO Role ([Description]) " +
                    "VALUES (@Description) " +
                    "SELECT * FROM Role WHERE Role.Id = (SELECT SCOPE_IDENTITY())"},
                { "RemoveQuery",
                    "DELETE FROM Role WHERE Id = @Id" },
                { "UpdateQuery",
                    "UPDATE Role SET " +
                    "[Description]=@Description " +
                    "WHERE Id = @Id"}
            };
            _db = projectDbContext;
        }

        public override IEnumerable<Role> Get(string queryWhere = null, object parameters = null)
        {
            IEnumerable<Role> Roles = null;

            _db.UsingConnection((dbCon) =>
            {
                Roles = dbCon.Query<Role>(
                "SELECT * FROM Role " +
                (queryWhere == null ? string.Empty : $" WHERE {queryWhere}"),
                param: parameters);
            });

            return Roles;
        }
        public Role GetUserRole()
        {
            return Get("Description=@Description", new { Description = "User" }).FirstOrDefault();
        }
    }
}
