using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeamProject.Models;

namespace TeamProject.Controllers
{
    public class HomeController : Controller
    {
        private ProjectDbContext db = new ProjectDbContext();

        // GET: Courts
        public ActionResult Index()
        {
            var court = db.Court.Get();//.Include(c => c.Branch);
            return View(court.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}