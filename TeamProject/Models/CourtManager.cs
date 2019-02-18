using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
namespace TeamProject.Models
{


    public class CourtManager : TableManager<Court>
    {
        public CourtManager(ProjectDbContext projectDbContext)
        {
            _queryParts = new Dictionary<string, string>()
            {
                { "FindById", "Court.id = @id" },
                { "InsertQuery",
                    "INSERT INTO Court ([BranchId],[Name],[ImageCourt],[MaxPlayers],[Price]) " +
                    "VALUES (@BranchId,@Name,@ImageCourt,@MaxPlayers,@Price)" +
                    "SELECT * FROM Court WHERE Court.Id = (SELECT SCOPE_IDENTITY())"},
                { "RemoveQuery",
                    "DELETE FROM Court WHERE Id = @Id" },
                { "UpdateQuery",
                    "UPDATE Court SET " +
                    "[BranchId]=@BranchId, [Name]=@Name, [ImageCourt]=@ImageCourt, [MaxPlayers]=@MaxPlayers, [Price]=@Price " +
                    "WHERE Id = @Id"}
            };
            _db = projectDbContext;
        }

        public override IEnumerable<Court> Get(string queryWhere = null, object parameters = null)
        {
            IEnumerable<Court> courts = null;

            _db.UsingConnection((dbCon) =>
            {
                courts = dbCon.Query<Court, Branch, Court>(
                    "SELECT * FROM Court INNER JOIN Branch ON Court.BranchId = Branch.Id" + (queryWhere == null ? string.Empty : $" WHERE {queryWhere}"),
                    (court, branch) =>
                    {
                        court.Branch = branch;
                        return court;
                    },
                    splitOn: "id",
                    param: parameters)
                    .Distinct();
            });

            return courts;
        }
    }

}