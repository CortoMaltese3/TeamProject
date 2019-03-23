﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TeamProject.Models;
using TeamProject.ModelsViews;

namespace TeamProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Owner")]
    public class BookingsController : Controller
    {
        private ProjectDbContext db = new ProjectDbContext();

        // GET: Bookings
        public ActionResult Index(BookingsSearchForm bookingSearchForm)
        {
            var model = new BookingsSearchResult()
            {
                BranchId = bookingSearchForm.Id,
                CourtId = bookingSearchForm.CourtId ?? db.Courts.BranchCourts(bookingSearchForm.Id).FirstOrDefault()?.Id ?? 0,
                FromDate = bookingSearchForm.FromDate ?? StartOfWeek(DateTime.Now),
                Courts = db.Courts.BranchCourts(bookingSearchForm.Id)
            };

            model.ToDate = bookingSearchForm.ToDate ?? model.FromDate.AddDays(6);

            model.TimeslotApiViews = db.TimeSlots
                .GetBookings(model.CourtId, model.FromDate, model.ToDate)
                .OrderBy(t => t.Hour);

            return View(model);

        }
        private DateTime StartOfWeek(DateTime dt)
        {
            int diff = (7 + (dt.DayOfWeek - DayOfWeek.Monday)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }

        // GET: Bookings/Details/5
        //public ActionResult Details(int? id, int? branchId)
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id ?? 0);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.BranchId = booking.Court.BranchId;
            return View(booking);
        }

        // GET: Bookings/Create
        public ActionResult Create()
        {
            ViewBag.CourtId = new SelectList(db.Courts.Get(), "Id", "Name");
            ViewBag.UserId = new SelectList(db.Users.Get(), "Id", "Firstname");
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CourtId,UserId,BookedAt,Duration")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Bookings.Add(booking);
                return RedirectToAction("Index");
            }

            ViewBag.CourtId = new SelectList(db.Courts.Get(), "Id", "Name", booking.CourtId);
            ViewBag.UserId = new SelectList(db.Users.Get(), "Id", "Firstname", booking.UserId);
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id ?? 0);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourtId = new SelectList(db.Courts.Get(), "Id", "Name", booking.CourtId);
            ViewBag.UserId = new SelectList(db.Users.Get(), "Id", "Firstname", booking.UserId);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CourtId,UserId,BookedAt,Duration")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Bookings.Update(booking);
                return RedirectToAction("Index");
            }
            ViewBag.CourtId = new SelectList(db.Courts.Get(), "Id", "Name", booking.CourtId);
            ViewBag.UserId = new SelectList(db.Users.Get(), "Id", "Firstname", booking.UserId);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id ?? 0);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.Bookings.Find(id);
            db.Bookings.Remove(id);
            return RedirectToAction("Index");
        }

    }
}
