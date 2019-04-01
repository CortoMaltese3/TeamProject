using System;
using System.Collections.Generic;
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
        private ProjectDbContext db = new ProjectDbContext();

        [Authorize(Roles = "Owner")]
        public BookingInfo GetBookingInfo(int id)
        {
            return db.Bookings
                .Get("Booking.Id=@id", new { id })
                .Select(b => new BookingInfo(b))
                .FirstOrDefault();
        }

        [Authorize(Roles = "Owner")]
        public IEnumerable<TimeslotApiView> GetCourtsForCalendarView(int? id, DateTime fromDate, DateTime toDate)
        {
            return db.TimeSlots
                .GetBookings(id ?? 0, fromDate, toDate)
                .OrderBy(t => t.Hour);
        }

        [Authorize(Roles = "Owner")]
        public Dictionary<string, List<Booking>> GetCourtsForListView(int? id, DateTime fromDate, DateTime toDate)
        {
            return db.Bookings
                .Get("CourtId=@CourtId AND BookedAt Between @fromDate And @toDate", new
                {
                    CourtId = id,
                    fromDate,
                    toDate
                })
                .OrderBy(b => b.BookedAt)
                .ThenBy(b => b.User.UserName)
                .GroupBy(b => b.BookedAt.ToLongDateString())
                .ToDictionary(g => g.Key, g => g.ToList());
        }
    }
}