using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TeamProject.Dal;
using TeamProject.Models;
using TeamProject.ModelsViews;

namespace TeamProject.Managers
{
    public class TimeSlotManager : TableManager<TimeSlot>
    {

        public TimeSlotManager(ProjectDbContext projectDbContext) 
        {
            _db = projectDbContext;
        }

        public override IEnumerable<TimeSlot> Get(string queryWhere = null, object parameters = null)
        {
            IEnumerable<TimeSlot> timeslots = null;

            _db.UsingConnection((dbCon) =>
            {
                timeslots = dbCon.Query<TimeSlot, Court, TimeSlot>(
                "SELECT * FROM TimeSlot " +
                "INNER JOIN Court ON TimeSlot.CourtId = Court.Id" + (queryWhere == null ? string.Empty : $" WHERE {queryWhere}"),
                (timeslot, court) =>
                {

                    timeslot.Court = court;
                    return timeslot;

                },

            splitOn: "id",
                    param: parameters)
                    .Distinct()
                    .ToList();
            });

            return timeslots;
        }


    }
}
