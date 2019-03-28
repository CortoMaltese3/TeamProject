using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeamProject.Models;

namespace TeamProject.Areas.Admin.Controllers
{
    public class ReportsController : Controller
    {
        private ProjectDbContext db = new ProjectDbContext();

        // GET: Reports
        [Authorize(Roles = "Admin, Owner")]
        public ActionResult Index(int id)
        {
            var branchreport = db.Branches.GetBookingsByBranchAndDay(id);
            //var courtsreport = db.Branches.GetBookingsByCourtAndDay(id);
            var courts = db.Courts.Get().Where(c => c.BranchId == id);
            ViewBag.Data = string.Join(",", branchreport.OrderBy(b => b.BookingDayNo).Select(b => b.CountOfBookings)) ;
            ViewBag.Labels = string.Join(",", branchreport.OrderBy(b => b.BookingDayNo).Select(b => "\"" + b.BookingDay + "\""));
            return View(courts);
        }
    }
}