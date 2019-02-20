using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamProject.Models
{
    public class FacilityManager : TableManager<Facility>
    {
        public FacilityManager(ProjectDbContext projectDbContext)
        {
            _queryParts = new Dictionary<string, string>()
            {
                { "FindById", "Facility.id = @id" },
                { "InsertQuery",
                    " INSERT INTO Facility ([Description]) " +
                    " VALUES (@Description) " +
                    " SELECT * FROM Facility WHERE Facility.Id = (SELECT SCOPE_IDENTITY()) "},
                { "RemoveQuery",
                    " DELETE FROM Facility WHERE Id = @Id" },
                { "UpdateQuery",
                    " UPDATE Facility SET " +
                    " [Description] = @Description " +
                    " WHERE Id = @Id"}
            };
            _db = projectDbContext;
        }

        public override IEnumerable<Facility> Get(string queryWhere = null, object parameters = null)
        {
            IEnumerable<Facility> facilities = null;

            var facilityDictionary = new Dictionary<int, Facility>();  

            _db.UsingConnection((dbCon) =>
            {                
                facilities = dbCon.Query<Facility, Branch, Facility>(
                    "SELECT Facility.*, Branch.* FROM Facility " +
                    " LEFT JOIN BranchFacilities ON Facility.Id = BranchFacilities.FacilityId " +
                    " LEFT JOIN Branch ON BranchFacilities.BranchId = Branch.Id " +
                    (queryWhere == null ? string.Empty : $" WHERE {queryWhere}"),
                    (facility, branch) =>
                    {
                        Facility facilityEntry;

                        if (!facilityDictionary.TryGetValue(facility.Id, out facilityEntry))
                        {
                            facilityEntry = facility;
                            facilityEntry.Branch  = new List<Branch>();
                            facilityDictionary.Add(facilityEntry.Id, facilityEntry);
                        }
                        if (branch != null)
                        {
                            facilityEntry.Branch.Add(branch);
                        }

                        return facilityEntry;
                    },
                    splitOn: "id,id",
                    param: parameters)
                    .Distinct();
            });

            return facilities;
        }
    }
}