using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using TeamProject.Models;
using TeamProject.ModelsViews;


namespace TeamProject.ApiControllers
{
    [Authorize]
    public class BookController : ApiController
    {
        private ProjectDbContext db = new ProjectDbContext();

        // GET: api/Book/5
        public IEnumerable<TimeslotApiView> Get(int id, DateTime fromDate, DateTime toDate)
        {
            return db.TimeSlots.GetForBooking(id, fromDate, toDate);
        }

        // POST: api/Book
        [HttpPost]
        public PostResponse Post(PutBookModel putBookModel)
        {
            if (!GetLoggedInUserId(out int loggedUserId))
            {
                return new PostResponse() { Status = "Empty Logged User" };
            }

            var booking = new Booking()
            {
                CourtId = putBookModel.CourtId,
                BookedAt = putBookModel.BookedAt.ToLocalTime(),
                UserId = loggedUserId,
                Duration = 60
            };
            //insert into booking (courtId , book) SELECT 3 AS CourtId, 'Monday' as book FROM (select count(*) as ExistsId from booking where courtId=1 and book='Monday') b where b.ExistsId=0
            booking = db.Bookings.Add(booking);

            return new PostResponse() { Status = db.LastActionStatus, BookingId = booking?.Id??0 };
        }
        private bool GetLoggedInUserId(out int loggedUserId)
        {
            var identity = User.Identity as ClaimsIdentity;
            var userDateId = identity.FindFirst(c => c.Type == ClaimTypes.UserData).Value;

            return int.TryParse(userDateId, out loggedUserId);
        }
        public class PostResponse
        {
            public string Status { get; set; }
            public int BookingId { get; set; }
        }
    }

}
