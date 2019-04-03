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


        public Booking GetBookingInfo(int id)
        {
            return db.Bookings.Find(id);
        }

        public IEnumerable<TimeslotApiView> GetCourtsForCalendarView(int? id, DateTime fromDate, DateTime toDate)
        {
            return db.TimeSlots
                .GetBookings(id ?? 0, fromDate, toDate)
                .OrderBy(t => t.Hour);
        }

        public Dictionary<string, List<Booking>> GetCourtsForListView(int? id, DateTime fromDate, DateTime toDate)
        {
            return app.GetCourtBookings(id ?? 0, fromDate, toDate)
                .OrderBy(b => b.BookedAt)
                .ThenBy(b => b.User.UserName)
                .GroupBy(b => b.BookedAt.ToLongDateString())
                .ToDictionary(g => g.Key, g => g.ToList());
        }

        public Dictionary<string, decimal> GetCourtPricesByMonth(int? id, DateTime fromDate, DateTime toDate)
        {
            var courtBookings = app.GetCourtBookings(id ?? 0, fromDate, toDate);

            return GroupBookingsByDateAndPrice(courtBookings);
        }

        public Dictionary<string, decimal> GetBranchPricesByMonth(int? id, DateTime fromDate, DateTime toDate)
        {
            var branchBookings = app.GetBranchBookings(id ?? 0, fromDate, toDate);

            return GroupBookingsByDateAndPrice(branchBookings);
        }

        public Dictionary<string, int> GetBranchBookingsByWeekDay(int? id, DateTime fromDate, DateTime toDate)
        {
            var bookingsReport = app.GetBranchBookings(id ?? 0, fromDate, toDate);

            return GetBookingsByWeekDay(bookingsReport);
        }

        public Dictionary<string, int> GetCourtBookingsByWeekDay(int? id, DateTime fromDate, DateTime toDate)
        {
            var bookingsReport = app.GetCourtBookings(id ?? 0, fromDate, toDate);

            return GetBookingsByWeekDay(bookingsReport);
        }

        private Dictionary<string, int> GetBookingsByWeekDay(IEnumerable<Booking> bookings)
        {
            return bookings
                .Select(b => new { WeekDayNo = b.BookedAt.ToString("d"), Group = b.BookedAt.ToString("dddd", CultureInfo.InvariantCulture) })
                .OrderBy(b => b.WeekDayNo)
                .GroupBy(b => b.Group)
                .ToDictionary(g => g.Key, g => g.Count());
        }

        private Dictionary<string, decimal> GroupBookingsByDateAndPrice(IEnumerable<Booking> bookings)
        {
            return bookings
                .OrderBy(b => b.BookedAt)
                .Select(b => new { Group = b.BookedAt.ToString("yyyy MMM", CultureInfo.InvariantCulture), b.Court.Price })
                .GroupBy(b => b.Group)
                .ToDictionary(g => g.Key, g => g.Sum(b => b.Price));
        }


    }
}
