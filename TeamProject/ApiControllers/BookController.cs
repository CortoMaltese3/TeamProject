using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TeamProject.ApiControllers
{
    public class BookController : ApiController
    {
        // GET: api/Book
        public IEnumerable<string> Get()
        {
            // TODO 
            // GET TIMESLOTS FOR
            // SET DATEFIRST 1;
            // SELECT *
            //  FROM[TimeSlot] LEFT JOIN Booking
            // ON TimeSlot.CourtId = Booking.CourtId
            // AND[TimeSlot].[Day] = DATEPART(WEEKDAY,[Booking].[BookedAt])
            // AND[TimeSlot].[Hour] = cast([Booking].[BookedAt] as time)
            // WHERE TimeSlot.CourtId = 1
            // AND([Booking].[BookedAt] BETWEEN '2019-03-11' AND '2019-03-17 23:59:59' OR Booking.Id is null)

            return new string[] { "value1", "value2" };
        }

        // GET: api/Book/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Book
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Book/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Book/5
        public void Delete(int id)
        {
        }
    }
}
