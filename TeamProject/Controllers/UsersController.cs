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
    public class UsersController : Controller
    {
        private ProjectDbContext db = new ProjectDbContext();

        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.Get());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = db.Users.Find(id??0);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Firstname,Lastname,Email,Password,Salt")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id??0);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Firstname,Lastname,Email,Password,Salt")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Update(user);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = db.Users.Find(id??0);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var user = db.Users.Find(id);
            db.Users.Remove(id);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }

        //public User Login(string username, string password)
        //{
        //    var loggedInUser = db.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
        //    if (loggedInUser != null)
        //    {
        //        var claims = new List<Claim>(new[]
        //        {
        //            // adding following 2 claim just for supporting default antiforgery provider
        //            new Claim(ClaimTypes.NameIdentifier, username),
        //            new Claim(
        //                "http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider",
        //                "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"),
        //            new Claim(ClaimTypes.Name, username),
        //            new Claim(ClaimTypes.Role,loggedInUser.Role.ToString())
        //        });



        //        var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

        //        HttpContext.Current.GetOwinContext().Authentication.SignIn(
        //            new AuthenticationProperties { IsPersistent = false }, identity);
        //    }

        //    return loggedInUser;
        //}

        //public bool UserExists(string username)
        //{
        //    return db.Users.Any(u => u.Username == username);
        //}
    }
}
