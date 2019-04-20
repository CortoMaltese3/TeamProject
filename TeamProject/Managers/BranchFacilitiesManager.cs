using Dapper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        //public IEnumerable<SelectListItem> GetFacilities(int branchId)
        //{
        //    // get already selected branch facilities
        //    var selectedFacilities = Get("BranchId = @branchId", new { branchId });

        //    // get list of all facilities and create a list of SelectListItem
        //    // with selected property = true if exists in selectedFacilities
        //    var allFacilities = _db.Facilities.Get().Select(f => new SelectListItem()
        //    {
        //        Text = f.Description,
        //        Value = f.Id.ToString(),
        //        Selected = selectedFacilities.Any(sf=>sf.FacilityId==f.Id)
        //    });

        //    return allFacilities.OrderBy(f=>f.Text);
        //}

    }
}