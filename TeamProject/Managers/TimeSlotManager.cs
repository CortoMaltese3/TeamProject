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
        public IEnumerable<TimeslotApiView> GetForBooking(int courtId, DateTime fromDate, DateTime toDate)
        {
            return GetForTimeSlotsPivot(courtId, fromDate, toDate, "GetTimeslotsAt");
        }



        public IEnumerable<TimeslotApiView> GetForView(int courtId)
        {
            IEnumerable<TimeslotApiView> courtTimeslots = Enumerable.Empty<TimeslotApiView>();

            _db.UsingConnection((dbCon) =>
            {
                courtTimeslots = dbCon
                    .Query<TimeslotApiView>("GetTimeslots",
                        new
                        {
                            CourtId = courtId
                        },
                        commandType: CommandType.StoredProcedure);
            });

            return courtTimeslots;
        }

        public IEnumerable<TimeslotApiView> GetBookings(int courtId, DateTime fromDate, DateTime toDate)
        {
            return GetForTimeSlotsPivot(courtId, fromDate, toDate, "GetBookingsAt");
        }

        private IEnumerable<TimeslotApiView> GetForTimeSlotsPivot(int courtId, DateTime fromDate, DateTime toDate, string procedure)
        {
            IEnumerable<TimeslotApiView> courtTimeslots = Enumerable.Empty<TimeslotApiView>();

            _db.UsingConnection((dbCon) =>
            {
                courtTimeslots = dbCon
                    .Query<TimeslotApiView>(procedure,
                        new
                        {
                            CourtId = courtId,
                            DateFrom = fromDate.ToString("yyyy-MM-dd"),
                            DateTo = toDate.ToString("yyyy-MM-dd 23:59:00.000")
                        },
                        commandType: CommandType.StoredProcedure);
            });

            return courtTimeslots;
        }

    }
}
