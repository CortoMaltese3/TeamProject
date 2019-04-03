using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using TeamProject.Dal;
using TeamProject.Models;
using TeamProject.ModelsViews;


namespace TeamProject.ApiControllers
{
    [Authorize(Roles = "Admin,Owner")]
    [Route("api/bookings/{action}/{id}")]
    public class BookingsController : ApiController
    {
        private TeamProjectApp app = new TeamProjectApp();
        private ProjectDbContext db = new ProjectDbContext();

        /// <summary>
        /// Get booking by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Booking GetBookingInfo(int id)
        {
            return db.Bookings.Find(id);
        }

        /// <summary>
        /// Get Timeslots by hour for each week day
        /// </summary>
        /// <param name="id">Booking Id</param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public IEnumerable<TimeslotApiView> GetCourtsForCalendarView(int? id, DateTime fromDate, DateTime toDate)
        {
            return db.TimeSlots
                .GetBookings(id ?? 0, fromDate, toDate)
                .OrderBy(t => t.Hour);
        }

        /// <summary>
        /// Get Bookings details by date 
        /// </summary>
        /// <param name="id">court id</param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public Dictionary<string, List<Booking>> GetCourtsForListView(int? id, DateTime fromDate, DateTime toDate)
        {
            return app.GetCourtBookings(id ?? 0, fromDate, toDate)
                .OrderBy(b => b.BookedAt)
                .ThenBy(b => b.User.UserName)
                .GroupBy(b => b.BookedAt.ToLongDateString())
                .ToDictionary(g => g.Key, g => g.ToList());
        }

        /// <summary>
        /// Get Prices by month in given period and court
        /// </summary>
        /// <param name="id">Court id</param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public Dictionary<string, decimal> GetCourtPricesByMonth(int? id, DateTime fromDate, DateTime toDate)
        {
            var courtBookings = app.GetCourtBookings(id ?? 0, fromDate, toDate);

            return GroupBookingsByDateAndPrice(courtBookings);
        }

        /// <summary>
        /// Get Prices by month in given period and branch
        /// </summary>
        /// <param name="id">Branch Id</param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public Dictionary<string, decimal> GetBranchPricesByMonth(int? id, DateTime fromDate, DateTime toDate)
        {
            var branchBookings = app.GetBranchBookings(id ?? 0, fromDate, toDate);

            return GroupBookingsByDateAndPrice(branchBookings);
        }

        /// <summary>
        /// Gets Branch bookings (count) by week day
        /// </summary>
        /// <param name="id">branch Id</param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public Dictionary<string, int> GetBranchBookingsByWeekDay(int? id, DateTime fromDate, DateTime toDate)
        {
            var bookingsReport = app.GetBranchBookings(id ?? 0, fromDate, toDate);

            return GetBookingsByWeekDay(bookingsReport);
        }

        /// <summary>
        /// Gets courts bookings (count) by week day
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public Dictionary<string, int> GetCourtBookingsByWeekDay(int? id, DateTime fromDate, DateTime toDate)
        {
            var bookingsReport = app.GetCourtBookings(id ?? 0, fromDate, toDate);

            return GetBookingsByWeekDay(bookingsReport);
        }

        #region ConvertionMethods
        /// <summary>
        /// Convert List of bookings to dictionary of counts of bookings per day
        /// </summary>
        /// <param name="bookings"></param>
        /// <returns></returns>
        private Dictionary<string, int> GetBookingsByWeekDay(IEnumerable<Booking> bookings)
        {
            return bookings
                .Select(b => new { WeekDayNo = b.BookedAt.ToString("d"), Group = b.BookedAt.ToString("dddd", CultureInfo.InvariantCulture), Count = b.Id == 0 ? 0 : 1 })
                .OrderBy(b => b.WeekDayNo)
                .GroupBy(b => b.Group)
                .ToDictionary(g => g.Key, g => g.Sum(b => b.Count));
        }

        /// <summary>
        /// Convert List of bookings to dictionary of Price of bookings per month
        /// </summary>
        /// <param name="bookings"></param>
        /// <returns></returns>
        private Dictionary<string, decimal> GroupBookingsByDateAndPrice(IEnumerable<Booking> bookings)
        {
            return bookings
                .OrderBy(b => b.BookedAt)
                .Select(b => new { Group = b.BookedAt.ToString("MM/yyyy", CultureInfo.InvariantCulture), b.Court.Price })
                .GroupBy(b => b.Group)
                .ToDictionary(g => g.Key, g => g.Sum(b => b.Price));
        }
        #endregion

    }
}
