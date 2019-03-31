using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeamProject.Dal;
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
            //var branchreport = db.Branches.GetBookingsByBranchAndDay(id);
            //var courtsreport = db.Branches.GetBookingsByCourtAndDay(id);
            var courts = db.Courts.Get().Where(c => c.BranchId == id);
            //ViewBag.Data = string.Join(",", branchreport.OrderBy(b => b.BookingDayNo).Select(b => b.CountOfBookings));
            //ViewBag.Labels = string.Join(",", branchreport.OrderBy(b => b.BookingDayNo).Select(b => "\"" + b.BookingDay + "\""));

            var report = new List<ReportView>();
            report.Add(new ReportView()
            {
                Id = 0,
                Title = "branch",
                Data = db.Branches.GetBookingsByBranchAndDay(id)
            });

            foreach (var court in courts)
            {
                report.Add(new ReportView()
                {
                    Id = court.Id,
                    Title = court.Name,
                    Data = db.Branches.GetBookingsByCourtAndDay(id)
                });
            }

            return View(report);
        }

    }
    public class ReportView
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IEnumerable<BookingReport> Data { get; set; }
    }
}
