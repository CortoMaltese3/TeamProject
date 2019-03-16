using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TeamProject.Models;
using TeamProject.ModelsViews;

namespace TeamProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BranchesController : Controller
    {
        private const double FIXED_DISTANCE = 10000;

        private ProjectDbContext db = new ProjectDbContext();
        

        // GET: Branches
        public ActionResult Index()
        {
            //return a list sorted by distance.
            List<Branch> branches = new List<Branch>();

            var branch = db.Branches.Get();//.Include(b => b.User);

            foreach (var bran in branch)
            {
                branches.Add(bran);
            }
            branches.Sort((x, y) => x.Distance.CompareTo(y.Distance));
            return View(branches.ToList());
        }

        // GET: Branches/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branch branch = db.Branches.Find(id ?? 0);
            if (branch == null)
            {
                return HttpNotFound();
            }
            return View(branch);
        }

        // GET: Branches/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users.Get(), "Id", "Firstname");
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
                db.Branches.Add(branch);
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users.Get(), "Id", "Firstname", branch.UserId);
            return View(branch);
        }

        // GET: Branches/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branch branch = db.Branches.Find(id ?? 0);
            if (branch == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users.Get(), "Id", "Firstname", branch.UserId);
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
                db.Branches.Update(branch);// Entry(branch).State = EntityState.Modified;
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users.Get(), "Id", "Firstname", branch.UserId);
            return View(branch);
        }

        // GET: Branches/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branch branch = db.Branches.Find(id ?? 0);
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
            Branch branch = db.Branches.Find(id);
            db.Branches.Remove(branch.Id);
            return RedirectToAction("Index");
        }

    }
}
