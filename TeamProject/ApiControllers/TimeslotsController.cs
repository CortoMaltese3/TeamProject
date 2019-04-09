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
    [Route("api/Timeslots/{action}")]
    public class TimeslotsController : ApiController
    {
        private ProjectDbContext db = new ProjectDbContext();

        /// <summary>
        /// Add new timeslot
        /// </summary>
        public string Add(TimeSlot timeSlot)
        {
            db.TimeSlots.Add(timeSlot);
            return db.LastActionStatus;
        }

        /// <summary>
        /// remove timeslot
        /// </summary>
        public string Remove(TimeSlot timeSlot)
        {
            db.TimeSlots.Remove(timeSlot.Id);
            return db.LastActionStatus;
        }

    }
}
