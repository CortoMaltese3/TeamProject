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
    [Authorize]
    //[Route("api/book/{id}")]
    public class BookController : ApiController
    {
        private ProjectDbContext db = new ProjectDbContext();
        private TeamProjectApp app = new TeamProjectApp();

        // GET: api/Book/5
        public IEnumerable<TimeslotApiView> Get(int id,  DateTime fromDate, DateTime toDate)
        {
            return app.GetForBooking(id, fromDate, toDate);
        }

        // POST: api/Book
        /// <summary>
        /// Add a new booking
        /// </summary>
        /// <param name="putBookModel"></param>
        /// <returns></returns>
        [HttpPost]
        public PostBookResponse Post(PutBookModel putBookModel)
        {
            if (!GetLoggedInUserId(out int loggedUserId))
            {
                return new PostBookResponse() { Status = "You must log in first" };
            }

            if (!putBookModel.IsValidDate())
            {
                return new PostBookResponse() { Status = $"Can't Book on date before {DateTime.Now.Date}" };
            }

            var booking = new Booking()
            {
                CourtId = putBookModel.CourtId,
                BookedAt = putBookModel.BookedAt.ToLocalTime(),
                UserId = loggedUserId,
                BookKey = Guid.NewGuid().ToString("N"),
                Duration = 60
            };

            //$"{guid.Substring(0, 4)}-{guid.Substring(6, 4)}-{putBookModel.CourtId.ToString("0000").Substring(0, 4)}";
            booking = db.Bookings.Add(booking);

            if (booking.Id == 0)
            {
                return new PostBookResponse() { Status = $"At <small class='text-muted'>{putBookModel.BookedAt.ToLocalTime()}</small>" };
            }

            return new PostBookResponse()
            {
                Status = db.LastActionStatus,
                BookingId = booking.Id,
                BookKey = booking.BookKey
            };
        }

        private bool GetLoggedInUserId(out int loggedUserId)
        {
            var identity = User.Identity as ClaimsIdentity;
            var userDateId = identity.FindFirst(c => c.Type == ClaimTypes.UserData).Value;

            return int.TryParse(userDateId, out loggedUserId);
        }

    }

}
