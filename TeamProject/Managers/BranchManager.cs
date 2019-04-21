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
    public class BranchManager : TableManager<Branch>
    {
        public BranchManager(ProjectDbContext projectDbContext) 
        {
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

                        if (!branchDictionary.TryGetValue(branch.Id, out Branch branchEntry))
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



    }
}