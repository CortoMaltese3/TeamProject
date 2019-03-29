using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using TeamProject.Dal;
using TeamProject.Models;

namespace TeamProject.Controllers
{
    public class UserBookingController : Controller
    {
        private ProjectDbContext db = new ProjectDbContext();

        // GET: UserBooking
        public ActionResult Index()
        {
            int loggedUserId;
            GetLoggedInUserId(out loggedUserId);

            var bookings = db.Bookings.Get().Where(b => b.UserId == loggedUserId);

            return View(bookings);
        }

        private bool GetLoggedInUserId(out int loggedUserId)
        {
            var identity = User.Identity as ClaimsIdentity;
            var userDateId = identity.FindFirst(c => c.Type == ClaimTypes.UserData).Value;

            return int.TryParse(userDateId, out loggedUserId);
        }

    }
}
