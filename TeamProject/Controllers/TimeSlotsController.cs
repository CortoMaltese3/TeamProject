using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TeamProject.Models;

namespace TeamProject.Controllers
{
    public class TimeSlotsController : ApiController
    {
        private IDatabaseManager db = new DatabaseManager();

        //[HttpGet]
        public IEnumerable<TimeSlot> GetTimeSlots()
        {
            return db.GetTimeSlots(DateTime.Today,1);
        }
    }
}
