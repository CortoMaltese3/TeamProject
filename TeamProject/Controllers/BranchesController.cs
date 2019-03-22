using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TeamProject.Dal;
using TeamProject.Models;
using TeamProject.ModelsViews;

namespace TeamProject.Controllers
{
    public class BranchesController : Controller
    {
        

        private TeamProjectApp app = new TeamProjectApp();

        public ActionResult Nearest(string latitude, string longitude)
        {
            return View(app.GetNearestBranches(latitude, longitude));
        }


    }
}
