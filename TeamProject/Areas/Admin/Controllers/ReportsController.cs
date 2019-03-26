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
        [Authorize (Roles = "Admin, Owner")]
        public ActionResult Index(int id)
        {

            var courts = db.Courts.Get().Where(c => c.BranchId == id);
            ViewBag.Data = "data: [300, 50, 100, 40, 120]";
            return View(courts);
        }
    }
}