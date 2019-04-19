using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using TeamProject.Dal;
using TeamProject.Models;

namespace TeamProject.Managers
{


    public class CourtManager : TableManager<Court>
    {
        public CourtManager(ProjectDbContext projectDbContext) 
        {
            AddField(t => t.BranchId);
            AddField(t => t.Name);
            AddField(t => t.ImageCourt);
            AddField(t => t.Description);
            AddField(t => t.MaxPlayers);
            AddField(t => t.Price);
            PrepareQueries();

            _db = projectDbContext;
        }

        public override IEnumerable<Court> Get(string queryWhere = null, object parameters = null)
        {
            IEnumerable<Court> courts = null;
            var courtDictionary = new Dictionary<int, Court>();
            _db.UsingConnection((dbCon) =>
            {
                courts = dbCon.Query<Court, Branch, TimeSlot, Court>(
                    "SELECT * FROM Court " +
                    "INNER JOIN Branch ON Court.BranchId = Branch.Id " +
                    "LEFT JOIN Timeslot ON Court.Id = TimeSlot.CourtId " + (queryWhere == null ? string.Empty : $" WHERE {queryWhere}"),
                    (court, branch, timeslot) =>
                    {
                        Court courtEntry;

                        if (!courtDictionary.TryGetValue(court.Id, out courtEntry))
                        {
                            courtEntry = court;
                            courtEntry.TimeSlot = new List<TimeSlot>();
                            courtDictionary.Add(courtEntry.Id, courtEntry);
                        }
                        if (timeslot != null)
                        {
                            courtEntry.TimeSlot.Add(timeslot);
                        }
                        courtEntry.Branch = branch;
                        return courtEntry;
                    },
                    splitOn: "id",
                    param: parameters)
                    .Distinct();
            });

            return courts;
        }
        public IEnumerable<Court> AllCourtsSameBranch(int courtId)
        {
            var branchId = Find(courtId).Branch.Id;
            return BranchCourts(branchId);
        }
        public IEnumerable<Court> BranchCourts(int branchId)
        {
            return Get("branchId=@branchId", new { branchId });
        }

    }

}