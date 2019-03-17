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
        public ActionResult Index()
        {
            var court = db.Courts.Get().ToList();

            return View(court);
        }
        public ActionResult Book(int id)
        {
            var courtsInSameBranch = db.Courts.AllCourtsSameBranch(id).ToList();

            var bookViewModel = new BookViewModel()
            {
                CourtId = id,
                Courts = courtsInSameBranch
            };
            return View(bookViewModel);
        }
    }
}