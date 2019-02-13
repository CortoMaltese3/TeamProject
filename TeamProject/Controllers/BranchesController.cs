using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TeamProject.Models;

namespace TeamProject.Controllers
{
    public class BranchesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Nearest(string latitude, string longitude)
        {
            try
            {
                if (!double.TryParse(latitude, out double latitudeFixed) ||
                    !double.TryParse(longitude, out double longitudeFixed))
                {

                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                BranchManager branchManager = new BranchManager();
                // TODO 
                // create Model with Nearest Branches Found + Distance
                //var nearestBranches = branchManager.GetNearestBranches(latitudeFixed, longitudeFixed);
                IEnumerable<BranchWithDistance> branches = db.Branch.Select(b => new BranchWithDistance {
                    ID = b.ID,
                    UserID= b.UserID,
                    Name = b.Name,
                    Longtitude = b.Longtitude,
                    Latitude = b.Latitude,
                    City = b.City,
                    Address = b.Address,
                    ZipCode = b.ZipCode,
                    Distance = 100
                    
                });//.Where(branch => nearestBranches.Any(nb => nb.ID == branch.ID));

                return View(branches);

            }
            catch (NotImplementedException)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotImplemented);
            }
        }
        // GET: Branches
        public ActionResult Index()
        {
            var branch = db.Branch.Include(b => b.User);
            return View(branch.ToList());
        }

        // GET: Branches/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branch branch = db.Branch.Find(id);
            if (branch == null)
            {
                return HttpNotFound();
            }
            return View(branch);
        }

        // GET: Branches/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.User, "ID", "Firstname");
            return View();
        }

        // POST: Branches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserID,Name,Longtitude,Latitude,Point,City,Address,ZipCode")] Branch branch)
        {
            if (ModelState.IsValid)
            {
                db.Branch.Add(branch);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.User, "ID", "Firstname", branch.UserID);
            return View(branch);
        }

        // GET: Branches/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branch branch = db.Branch.Find(id);
            if (branch == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.User, "ID", "Firstname", branch.UserID);
            return View(branch);
        }

        // POST: Branches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserID,Name,Longtitude,Latitude,Point,City,Address,ZipCode")] Branch branch)
        {
            if (ModelState.IsValid)
            {
                db.Entry(branch).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.User, "ID", "Firstname", branch.UserID);
            return View(branch);
        }

        // GET: Branches/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branch branch = db.Branch.Find(id);
            if (branch == null)
            {
                return HttpNotFound();
            }
            return View(branch);
        }

        // POST: Branches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Branch branch = db.Branch.Find(id);
            db.Branch.Remove(branch);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private double GetDistance(Branch branch, double latitude, double longtitude)
        {
            const double earthRadius = 6378137;

            var dLat = ConvertDegreesToRadians(branch.Latitude - latitude);
            var dLong = ConvertDegreesToRadians(branch.Longtitude - longtitude);
            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(ConvertDegreesToRadians(latitude)) *
                Math.Cos(ConvertDegreesToRadians(branch.Latitude)) *
                Math.Sin(dLong / 2) * Math.Sin(dLong / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var d = (earthRadius * c) / 1000;
            return d; // returns the distance in meter
        }
        private double ConvertDegreesToRadians(double degrees)
        {
            double radians = (Math.PI / 180) * degrees;
            return (radians);
        }
    }
}
