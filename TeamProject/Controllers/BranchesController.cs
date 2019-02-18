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

namespace TeamProject.Controllers
{
    public class BranchesController : Controller
    {
        private const double FIXED_DISTANCE = 10000;

        private ProjectDbContext db = new ProjectDbContext();
        public ActionResult Nearest(string latitude, string longitude)
        {
            try
            {
                if (!double.TryParse(latitude, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out double latitudeFixed) ||
                    !double.TryParse(longitude, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out double longitudeFixed))
                {

                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                BranchManager branchManager = new BranchManager(new ProjectDbContext());
                IEnumerable<Branch> branches = db.Branch.Nearest(latitudeFixed, longitudeFixed, FIXED_DISTANCE);

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
            var branch = db.Branch.Get();//.Include(b => b.User);
            return View(branch.ToList());
        }

        // GET: Branches/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branch branch = db.Branch.Find(id??0);
            if (branch == null)
            {
                return HttpNotFound();
            }
            return View(branch);
        }

        // GET: Branches/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.User.Get(), "Id", "Firstname");
            return View();
        }

        // POST: Branches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,Name,Longitude,Latitude,City,Address,ZipCode")] Branch branch)
        {
            if (ModelState.IsValid)
            {
                db.Branch.Add(branch);
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.User.Get(), "Id", "Firstname", branch.UserId);
            return View(branch);
        }

        // GET: Branches/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branch branch = db.Branch.Find(id??0);
            if (branch == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.User.Get(), "Id", "Firstname", branch.UserId);
            return View(branch);
        }

        // POST: Branches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,Name,Longitude,Latitude,City,Address,ZipCode")] Branch branch)
        {
            if (ModelState.IsValid)
            {
                db.Branch.Update(branch);// Entry(branch).State = EntityState.Modified;
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.User.Get(), "Id", "Firstname", branch.UserId);
            return View(branch);
        }

        // GET: Branches/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branch branch = db.Branch.Find(id??0);
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
            db.Branch.Remove(branch.Id);
            return RedirectToAction("Index");
        }

    }
}
