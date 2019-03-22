using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeamProject.Models;
using TeamProject.ModelsViews;

namespace TeamProject.Controllers
{
    public class CourtsController : Controller
    {
        private ProjectDbContext db = new ProjectDbContext();
        // GET: Courts
        public ActionResult Index(int? id)
        {
            var court = db.Courts.Get().ToList();
            if(id != null)
            {
                court = db.Courts.Get().Where(x => x.BranchId == id).ToList();
            }
            return View(court);
        }
        [Authorize]
        public ActionResult Booking(int id)
        {
            var courtsInSameBranch = db.Courts.AllCourtsSameBranch(id).ToList();

            var bookViewModel = new BookViewModel()
            {
                CourtId = id,
                Courts = courtsInSameBranch
            };
            return View(bookViewModel);
        }


        public ActionResult Confirmed(string BookKey)
        {
            var booking = db.Bookings.Get().Where(x => x.BookKey == BookKey).FirstOrDefault();
            var branch = db.Branches.Get().Where(x => x.Id == booking.Court.BranchId).FirstOrDefault();
            ViewBag.address = branch.Address;
            ViewBag.city = branch.City;

            SmtpMessageChunk.SendMessageSmtp(booking, branch);

            return View(booking);
        }
    }
}