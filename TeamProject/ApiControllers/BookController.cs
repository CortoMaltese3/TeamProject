using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using TeamProject.Models;
using TeamProject.ModelsViews;


namespace TeamProject.ApiControllers
{
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
            var booking = new Booking()
            {
                CourtId = putBookModel.CourtId,
                BookedAt = putBookModel.BookedAt.ToLocalTime(),
                UserId = putBookModel.UserId,
                Duration = 60
            };

            db.Bookings.Add(booking);
            
            return new PostResponse() { Status=db.LastActionStatus};
        }

        public class PostResponse
        {
            public string Status { get; set; }
            public string BookKey { get; set; }
        }
    }

}
