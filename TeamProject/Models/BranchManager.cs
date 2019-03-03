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
                branches = dbCon.Query<Branch, User, Facility, Court, Branch>(
                    "SELECT Branch.*, [User].*, Facility.*, Court.*  FROM Branch " +
                    " INNER JOIN [USER] ON Branch.UserId = [User].Id " +
                    " LEFT JOIN BranchFacilities ON Branch.Id = BranchFacilities.BranchId " +
                    " LEFT JOIN Facility ON BranchFacilities.FacilityId = Facility.Id " +
                    " LEFT JOIN Court ON Branch.Id = Court.BranchId" + (queryWhere == null ? string.Empty : $" WHERE {queryWhere}"),
                    (branch, user, facility, court) =>
                    {
                        Branch branchEntry;

                        if (!branchDictionary.TryGetValue(branch.Id, out branchEntry))
                        {
                            branchEntry = branch;
                            branchEntry.Court = new List<Court>();
                            branchEntry.Facility = new List<Facility>();
                            branchDictionary.Add(branchEntry.Id, branchEntry);
                        }

                        branchEntry.User = user;

                        if (court != null)
                        {
                            if (!branchEntry.Court.Any(c => c.Id == court.Id))
                            {
                                branchEntry.Court.Add(court);
                            }

                        }
                        if (facility != null)
                        {
                            if (!branchEntry.Facility.Any(f => f.Id == facility.Id))
                            {
                                branchEntry.Facility.Add(facility);
                            }
                        }

                        return branchEntry;
                    },
                        splitOn: "id,id,id",
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
                branches = dbCon.Query<Branch, Court, Branch>(
                    "GetBranchesDistance",
                    (branch, court) =>
                    {
                        Branch branchEntry;

                        if (!branchDictionary.TryGetValue(branch.Id, out branchEntry))
                        {
                            branchEntry = branch;
                            branchEntry.ImageCourt = court.ImageCourt;
                            branchDictionary.Add(branchEntry.Id, branchEntry);
                        }

                        return branchEntry;
                    },
                    splitOn: "id",
                    param: new { Latitude = latitude, Longitude = longitude, Distance = distanceInMeters },
                    commandType: CommandType.StoredProcedure).Distinct();
            });
            return branches;
        }
    }
}