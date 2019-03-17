using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TeamProject.Models;
using TeamProject.ModelsViews;

namespace TeamProject.Controllers
{
    public class BranchesController : Controller
    {
        private const double FIXED_DISTANCE = 20000;

        private ProjectDbContext db = new ProjectDbContext();

        public ActionResult Nearest(string latitude, string longitude)
        {

            if (!double.TryParse(latitude, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out double latitudeFixed) ||
                !double.TryParse(longitude, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out double longitudeFixed))
            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            BranchManager branchManager = new BranchManager(new ProjectDbContext());
            IEnumerable<Branch> branches = db.Branches.Nearest(latitudeFixed, longitudeFixed, FIXED_DISTANCE);

            return View(new NearestBrachView()
            {
                Latitude = latitudeFixed,
                Longitude = longitudeFixed,
                Branches = branches
            });

        }

    }
}
