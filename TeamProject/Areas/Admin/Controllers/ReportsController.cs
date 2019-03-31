using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeamProject.Areas.Admin.ViewModels;
using TeamProject.Dal;

namespace TeamProject.Areas.Admin.Controllers
{
    public class ReportsController : Controller
    {
        private ProjectDbContext db = new ProjectDbContext();

        // GET: Reports
        [Authorize(Roles = "Admin, Owner")]
        public ActionResult Index(int id)
        {
            var courts = db.Courts.Get().Where(c => c.BranchId == id);
            var report = new List<ReportView>()
            {
                new ReportView()
                {
                    Id = 0,
                    Title = "Branch",
                    BookingReport = db.Branches.GetBookingsByBranchAndDay(id)
                }
            };

            foreach (var court in courts)
            {
                report.Add(new ReportView()
                {
                    Id = court.Id,
                    Title = court.Name,
                    BookingReport = db.Branches.GetBookingsByCourtAndDay(id)
                });
            }

            return View(report);
        }

    }

}
