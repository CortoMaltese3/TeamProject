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

        /// <summary>
        /// Gets Courts Timeslots availability by Hour for each week day (day 1..7)
        /// if a timeslot is booked then day 1..7 value should be equal to '2'
        /// else if it is available the value should be equal to '1'
        /// if there is no timeslot has been set for givel hour and day 1..7 the value should be null
        /// </summary>
        /// <param name="courtId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public IEnumerable<TimeslotApiView> GetForBooking(int courtId, DateTime fromDate, DateTime toDate)
        {
            return GetForTimeSlotsPivot(courtId, fromDate, toDate, "GetTimeslotsAt");
        }

        /// <summary>
        /// Gets Courts Timeslots set by Hour for each week day (day 1..7)
        /// day 1..7 should have the null value if no timeslot has been set
        /// or the Id number of the Timeshot record
        /// </summary>
        /// <param name="courtId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets Courts Bookings by Hour for each week day (day 1..7)
        /// Day 1..7 value should be null if no timeslot is set at given hour/day
        /// or zero (0) if a timeslot is set but there is no booking 
        /// and finally equal to the Id of the Booking record 
        /// </summary>
        /// <param name="courtId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public IEnumerable<TimeslotApiView> GetBookings(int courtId, DateTime fromDate, DateTime toDate)
        {
            return GetForTimeSlotsPivot(courtId, fromDate, toDate, "GetBookingsAt");
        }

        /// <summary>
        /// Gets a list of TimeslotApiView for a courtId and a given date period
        /// using procedure from the parameters
        /// </summary>
        /// <param name="courtId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="procedure"></param>
        /// <returns></returns>
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
