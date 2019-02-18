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
    public class TimeSlotsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TimeSlots
        public ActionResult Index()
        {
            var timeSlot = db.TimeSlot.Include(t => t.Court);
            return View(timeSlot.ToList());
        }

        // GET: TimeSlots/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeSlot timeSlot = db.TimeSlot.Find(id);
            if (timeSlot == null)
            {
                return HttpNotFound();
            }
            return View(timeSlot);
        }

        // GET: TimeSlots/Create
        public ActionResult Create()
        {
            ViewBag.CourtId = new SelectList(db.Court, "Id", "Name");
            return View();
        }

        // POST: TimeSlots/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CourtId,Day,Hour,Duration")] TimeSlot timeSlot)
        {
            if (ModelState.IsValid)
            {
                db.TimeSlot.Add(timeSlot);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourtId = new SelectList(db.Court, "Id", "Name", timeSlot.CourtId);
            return View(timeSlot);
        }

        // GET: TimeSlots/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeSlot timeSlot = db.TimeSlot.Find(id);
            if (timeSlot == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourtId = new SelectList(db.Court, "Id", "Name", timeSlot.CourtId);
            return View(timeSlot);
        }

        // POST: TimeSlots/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CourtId,Day,Hour,Duration")] TimeSlot timeSlot)
        {
            if (ModelState.IsValid)
            {
                db.Entry(timeSlot).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourtId = new SelectList(db.Court, "Id", "Name", timeSlot.CourtId);
            return View(timeSlot);
        }

        // GET: TimeSlots/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeSlot timeSlot = db.TimeSlot.Find(id);
            if (timeSlot == null)
            {
                return HttpNotFound();
            }
            return View(timeSlot);
        }

        // POST: TimeSlots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TimeSlot timeSlot = db.TimeSlot.Find(id);
            db.TimeSlot.Remove(timeSlot);
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
    }
}
