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
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/Book/5
        public IEnumerable<TimeslotApiView> Get(int id)
        {
            return db.TimeSlots.GetForBooking(id);
        }

        //// POST: api/Book
        //public void Post([FromBody]string value)
        //{
        //}

        // PUT: api/Book/5
        public void Put(int id, PutView putView)
        {
            var booking = new Booking()
            {
                CourtId = putView.CourtId,
                BookedAt = putView.BookedAt,
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
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public int CourtId { get; set; }
        public int UserId { get; set; }

        public DateTime BookedAt { get => new DateTime(Year, Month, Day, Hour, 0, 0); }
    }
}
