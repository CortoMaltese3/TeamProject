using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
namespace TeamProject.Models
{
    public class BranchManager : TableManager<Branch>
    {
        public BranchManager(ProjectDbContext projectDbContext)
        {
            _queryParts = new Dictionary<string, string>()
            {
                { "FindById", "Branch.id = @id" },
                { "InsertQuery",
                    "INSERT INTO Branch ([UserId],[Name],[Longitude],[Latitude],[City],[Address],[ZipCode]) " +
                    "VALUES (@UserId,@Name,@Longitude,@Latitude,@City,@Address,@ZipCode)" +
                    "SELECT * FROM Branch WHERE Branch.Id = (SELECT SCOPE_IDENTITY())"},
                { "RemoveQuery",
                    "DELETE FROM Branch WHERE Id = @Id" },
                { "UpdateQuery",
                    "UPDATE Branch SET " +
                    "[UserId]=@UserId,[Name]=@Name,[Longitude]=@Longitude,[Latitude]=@Latitude,[City]=@City,[Address]=@Address,[ZipCode]=@ZipCode " +
                    "WHERE Id = @Id"}
            };
            _db = projectDbContext;
        }

        public override IEnumerable<Branch> Get(string queryWhere = null, object parameters = null)
        {
            IEnumerable<Branch> branches = null;

            var branchDictionary = new Dictionary<int, Branch>();
            _db.UsingConnection((dbCon) =>
            {
                branches = dbCon.Query<Branch, Court, Branch>(
                    "SELECT * FROM Branch LEFT JOIN Court ON Branch.Id = Court.BranchId" + (queryWhere == null ? string.Empty : $" WHERE {queryWhere}"),
                    (branch, court) =>
                    {
                        Branch branchEntry;

                        if (!branchDictionary.TryGetValue(branch.Id, out branchEntry))
                        {
                            branchEntry = branch;
                            branchEntry.Court = new List<Court>();
                            branchDictionary.Add(branchEntry.Id, branchEntry);
                        }

                        branchEntry.Court.Add(court);
                        return branchEntry;
                    },
                        splitOn: "id",
                        param: parameters)
                        .Distinct()
                        .ToList();
            });

            return branches;
        }

        /// <summary>
        /// Returns List of branches near to a given latitude, longtitude
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longtitude"></param>
        /// <returns></returns>
        public IEnumerable<Branch> Nearest(double latitude, double longitude, double distanceInMeters = 5000)
        {
            IEnumerable<Branch> branches = null;

            var branchDictionary = new Dictionary<int, Branch>();

            _db.UsingConnection((dbCon) =>
            {
                branches = dbCon.Query<Branch>(
                    "GetBranchesDistance",
                    new { Latitude = latitude, Longitude = longitude, Distance = distanceInMeters },
                    commandType: CommandType.StoredProcedure);
            });

            return branches;
        }
    }
}