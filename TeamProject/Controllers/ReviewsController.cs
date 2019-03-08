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
    public class ReviewsController : Controller
    {
        private ProjectDbContext db = new ProjectDbContext();

        // GET: Reviews
        public ActionResult Index()
        {
            var review = db.Reviews.Get();//.Include(r => r.Court).Include(r => r.User);
            return View(review.ToList());
        }

        // GET: Reviews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id ?? 0);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // GET: Reviews/Create
        public ActionResult Create()
        {
            ViewBag.CourtId = new SelectList(db.Courts.Get(), "Id", "Name");
            ViewBag.UserId = new SelectList(db.Users.Get(), "Id", "Firstname");
            return View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CourtId,UserId,Rating,Comment,CommentAt")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Reviews.Add(review);
                return RedirectToAction("Index");
            }

            ViewBag.CourtId = new SelectList(db.Courts.Get(), "Id", "Name", review.CourtId);
            ViewBag.UserId = new SelectList(db.Users.Get(), "Id", "Firstname", review.UserId);
            return View(review);
        }

        // GET: Reviews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id ?? 0);
            if (review == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourtId = new SelectList(db.Courts.Get(), "Id", "Name", review.CourtId);
            ViewBag.UserId = new SelectList(db.Users.Get(), "Id", "Firstname", review.UserId);
            return View(review);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CourtId,UserId,Rating,Comment,CommentAt")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Reviews.Update(review);
                return RedirectToAction("Index");
            }
            ViewBag.CourtId = new SelectList(db.Courts.Get(), "Id", "Name", review.CourtId);
            ViewBag.UserId = new SelectList(db.Users.Get(), "Id", "Firstname", review.UserId);
            return View(review);
        }

        // GET: Reviews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id ?? 0);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Review review = db.Reviews.Find(id);
            db.Reviews.Remove(id);
            return RedirectToAction("Index");
        }

    }
}
