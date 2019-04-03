using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeamProject.Dal;
using TeamProject.Managers;
using TeamProject.Models;

namespace TeamProject.Managers
{
    public class BranchFacilitiesManager : TableManager<BranchFacilities>
    {
        public BranchFacilitiesManager(ProjectDbContext projectDbContext)
        {
            _queryParts = new Dictionary<string, string>()
            {
                { "InsertQuery",
                    "INSERT INTO BranchFacilities ([BranchId], [FacilityId]) " +
                    "VALUES (@BranchId, @FacilityId) " +
                    "SELECT * FROM BranchFacilities WHERE BranchId=@BranchId AND FacilityId=@FacilityId"},
                { "UpdateQuery",
                    "UPDATE BranchFacilities SET " +
                    "[BranchId]=@BranchId, [FacilityId]=@FacilityId " +
                    "WHERE BranchId = @Id"}
            };
            _db = projectDbContext;
        }

        public override IEnumerable<BranchFacilities> Get(string queryWhere = null, object parameters = null)
        {
            IEnumerable<BranchFacilities> BranchFacilities = null;

            _db.UsingConnection((dbCon) =>
            {
                BranchFacilities = dbCon.Query<BranchFacilities, Branch, Facility, BranchFacilities>(
                    "SELECT BranchFacilities.*, [Branch].*, [Facility].* " +
                    "FROM BranchFacilities " +
                    "INNER JOIN [Branch] ON BranchFacilities.BranchId = [Branch].Id " +
                    "INNER JOIN [Facility] ON BranchFacilities.FacilityId = [Facility].Id " +
                    (queryWhere == null ? string.Empty : $" WHERE {queryWhere}"),
                    (BranchFacility, Branch, Facility) =>
                    {
                        BranchFacility.Branch = Branch;
                        BranchFacility.Facility = Facility;
                        return BranchFacility;
                    },
                    splitOn: "id",
                    param: parameters)
                    .Distinct();
            });

            return BranchFacilities;
        }
        public bool Remove(int BranchId, int FacilityId)
        {
            int affectedRows = 0;
            _db.UsingConnection((dbCon) =>
            {
                affectedRows = dbCon.Execute("DELETE " +
                    "FROM BranchFacilities " +
                    "WHERE BranchId = @BranchId " +
                    "AND FacilityId = @FacilityId",
                    new { BranchId, FacilityId });
            });
            return affectedRows > 0;
        }

        public IList<SelectListItem> GetFacilities(int branchId)
        {
            var allFacilities = _db.Facilities.Get().Select(f => new SelectListItem()
            {
                Text = f.Description,
                Value = f.Id.ToString()
            });

            var selectedFacilities = Get()
                .Where(f => f.BranchId == branchId)
                .Select(f => new SelectListItem()
                {
                    Value = f.FacilityId.ToString()
                });

            return allFacilities.Select((f) =>
            {
                if (selectedFacilities.Any(bf => bf.Value == f.Value))
                {
                    f.Selected = true;
                }
                return f;
            }).ToList();
        }
    }
}