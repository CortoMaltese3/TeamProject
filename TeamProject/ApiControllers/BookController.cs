using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        public IHttpActionResult Post(PutBookModel putBookModel)
        {
            var booking = new Booking()
            {
                CourtId = putBookModel.CourtId,
                BookedAt = putBookModel.BookedAt.ToLocalTime(),
                UserId = putBookModel.UserId,
                Duration = 60
            };
            db.Bookings.Add(booking);

            return Ok();
        }

    }
}
