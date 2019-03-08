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
        private ProjectDbContext db = new ProjectDbContext();

        // GET: TimeSlots
        public ActionResult Index(int? id)
        {
            var timeSlot = db.TimeSlots.Get().Where(t => t.CourtId == (id ?? 0));//.Include(t => t.Court);
            ViewBag.id = id;
            return View(timeSlot.ToList());
        }

        // GET: TimeSlots/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeSlot timeSlot = db.TimeSlots.Find(id ?? 0);
            if (timeSlot == null)
            {
                return HttpNotFound();
            }
            //ViewBag.id = timeSlot.CourtId;
            return View(timeSlot);
        }

        // GET: TimeSlots/Create
        public ActionResult Create(int? id)
        {
            ViewBag.CourtId = new SelectList(db.Courts.Get().Where(c => c.Id == (id ?? 0)), "Id", "Name");
            ViewBag.id = id;
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
                db.TimeSlots.Add(timeSlot);
                return RedirectToAction("Index", new { id = timeSlot.CourtId });
            }

            ViewBag.CourtId = new SelectList(db.Courts.Get(), "Id", "Name", timeSlot.CourtId);
            ViewBag.id = timeSlot.CourtId;
            return View(timeSlot);
        }

        // GET: TimeSlots/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeSlot timeSlot = db.TimeSlots.Find(id ?? 0);
            if (timeSlot == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourtId = new SelectList(db.Courts.Get(), "Id", "Name", timeSlot.CourtId);
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
                db.TimeSlots.Update(timeSlot);
                return RedirectToAction("Index", new { id = timeSlot.CourtId });
            }
            ViewBag.CourtId = new SelectList(db.Courts.Get(), "Id", "Name", timeSlot.CourtId);
            return View(timeSlot);
        }

        // GET: TimeSlots/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeSlot timeSlot = db.TimeSlots.Find(id ?? 0);
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
            TimeSlot timeSlot = db.TimeSlots.Find(id);

            db.TimeSlots.Remove(id);
            return RedirectToAction("Index", new { id = timeSlot.CourtId });
        }

    }
}
