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
        // GET: api/Book
        //public IEnumerable<string> Get()
        //{
        //}

        // GET: api/Book/5
        public IEnumerable<TimeslotApiView> Get(int id, DateTime fromDate, DateTime toDate)
        {
            return db.TimeSlots.GetForBooking(id, fromDate, toDate);
        }

        //// POST: api/Book
        //public void Post([FromBody]string value)
        //{
        //}

        // PUT: api/Book/5
        public void Put(PutView putView)
        {
            var booking = new Booking()
            {
                CourtId = putView.CourtId,
                BookedAt = putView.BookedAt.ToLocalTime(),
                UserId = putView.UserId,
                Duration = 60
            };
            db.Bookings.Add(booking);
        }

        //// DELETE: api/Book/5
        //public void Delete(int id)
        //{
        //}
    }
    public class PutView
    {
        public int CourtId { get; set; }
        public int UserId { get; set; }

        public DateTime BookedAt { get; set; }
    }

}
