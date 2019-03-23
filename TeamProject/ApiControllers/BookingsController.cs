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
    [Authorize(Roles = "Admin,Owner")]
    public class BookingsController : ApiController
    {
        private ProjectDbContext db = new ProjectDbContext();
        // GET: api/Bookings/5
        public User Get(int id)
        {
            return db.Bookings
                .Get("Booking.Id=@id", new { id })
                .FirstOrDefault()
                .User;
        }
    }
}