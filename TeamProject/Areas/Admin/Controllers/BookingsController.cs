using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TeamProject.Models;
using TeamProject.ModelsViews;
using TeamProject.Dal;

namespace TeamProject.Areas.Admin.Controllers
{

    [Authorize(Roles = "Owner")]
    public class BookingsController : Controller
    {
        private ProjectDbContext db = new ProjectDbContext();
        private TeamProjectApp app = new TeamProjectApp();

        // GET: Bookings
        public ActionResult Index(int? id)
        {
            var branchCourts = app.BranchCourts(id ?? 0);

            return View(branchCourts);
        }

        // GET: Bookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id ?? 0);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.BranchId = booking.Court.BranchId;
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id ?? 0);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.BranchId = booking.Court.BranchId;
            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.Bookings.Find(id);
            db.Bookings.Remove(id);
            return RedirectToAction("Index");
        }

    }
}
