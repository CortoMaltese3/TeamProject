using System.Net;
using System.Web.Mvc;
using TeamProject.Models;
using System.Linq;
using System;
using TeamProject.Dal;
using System.Collections.Generic;

namespace TeamProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private TeamProjectApp app = new TeamProjectApp();

        // GET: Users
        public ActionResult Index()
        {
            return View(app.GetAllUsers());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (!app.GetUserById(id ?? 0, out User user))
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
        public ActionResult Create(User user)
        {
            (bool valid, string field, string error) = app.AddUser(user, ModelState.IsValid);

            if (!valid)
            {
                ModelState.AddModelError(field, error);
                return View(user);
            }
            return RedirectToAction("Index");
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!app.GetUserById(id ?? 0, out User user))
            {
                return HttpNotFound();
            }

            // find roles that are not enabled for 
            // the selected user and add them to 
            // user.Roles list with IsNew property = true
            app.AddNotEnabledRolesToUser(user);

            // order by description
            user.Roles = user.Roles.OrderBy(r => r.Description).ToList();

            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            // update user and add or remove selected roles
            app.UpdateUserAndRoles(user);

            return RedirectToAction("Index");
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!app.GetUserById(id ?? 0, out User user))
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
            app.RemoveUserById(id);
            return RedirectToAction("Index");
        }



    }
}
