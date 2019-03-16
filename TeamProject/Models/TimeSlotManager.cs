using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TeamProject.ModelsViews;

namespace TeamProject.Models
{
    public class TimeSlotManager : TableManager<TimeSlot>
    {

        public TimeSlotManager(ProjectDbContext projectDbContext)
        {
            _queryParts = new Dictionary<string, string>()
            {
                { "FindById", "TimeSlot.id = @id" },
                { "InsertQuery",
                    "INSERT INTO TimeSlot ([CourtId],[Day],[Hour],[Duration]) " +
                    "VALUES (@CourtId,@Day,@Hour,@Duration) " +
                    "SELECT * FROM TimeSlot WHERE TimeSlot.Id = (SELECT SCOPE_IDENTITY())"},
                { "RemoveQuery",
                    "DELETE FROM TimeSlot WHERE Id = @Id" },
                { "UpdateQuery",
                    "UPDATE TimeSlot SET " +
                    "[CourtId]=@CourtId,[Day]=@Day,[Hour]=@Hour,[Duration]=@Duration " +
                    "WHERE Id = @Id"}
            };
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
        public IEnumerable<TimeslotApiView> GetForBooking(int courtId)
        {
            IEnumerable<TimeslotApiView> courtTimeslots = Enumerable.Empty<TimeslotApiView>();
            _db.UsingConnection((dbCon) =>
            {
                courtTimeslots = dbCon
                    .Query<TimeslotApiView>("GetTimeslotsAt",
                        new { CourtId = courtId, DateFrom = "2019-03-01", DateTo = "2019-03-17 23:59:59" },
                        commandType: CommandType.StoredProcedure);
            });

            return courtTimeslots;
        }
    }
}
