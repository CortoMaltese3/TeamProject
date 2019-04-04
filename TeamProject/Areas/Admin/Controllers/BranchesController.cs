using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using TeamProject.Dal;
using TeamProject.Models;
using TeamProject.ModelsViews;

namespace TeamProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, Owner")]
    public class BranchesController : Controller
    {
        private ProjectDbContext db = new ProjectDbContext();

        // GET: Branches
        public ActionResult Index()
        {
            var branches = db.Branches.Get().ToList() ;
            
            // If Owner filter only owners branches
            if (!User.IsInRole("Admin"))
            {
                var ownerUser = Session["User"] as User;
                branches = db.Branches.Get().Where(b => b.UserId == ownerUser.Id).ToList();
            }

            foreach(var item in branches)
            {
                if(item.ImageBranch==null)
                {
                    item.ImageBranch = "na_image.jpg";
                }
            }

            return View(branches);
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
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            GetLoggedInUserId(out int userId);
            return View(new Branch() { UserId = userId });
        }

        // POST: Branches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(Branch branch)
        {
            if (branch.ImageFile == null)
            {
                branch.ImageBranch = "na_image.jpg";
            }
            else
            {
                branch.ImageBranch = Path.GetFileName(branch.ImageFile.FileName);
                string fileName = Path.Combine(Server.MapPath("~/Images/Branches/"), branch.ImageBranch);
                branch.ImageFile.SaveAs(fileName);
            }
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
            ViewBag.UserId = new SelectList(db.Users.Get(), "Id", "UserName", branch.UserId);
            return View(branch);
        }

        // POST: Branches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Branch branch)
        {
            if (branch.ImageBranch == null)
            {
                branch.ImageBranch = "na_image.jpg";
            }

            if (branch.ImageFile == null)
            {

            }
            else
            { 

                branch.ImageBranch = Path.GetFileName(branch.ImageFile.FileName);
                string fileName = Path.Combine(Server.MapPath("~/Images/Branches/"), branch.ImageBranch);
                branch.ImageFile.SaveAs(fileName);
            }

            if (ModelState.IsValid)
            {
                db.Branches.Update(branch);
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users.Get(), "Id", "UserName", branch.UserId);
            return View(branch);
        }

        // GET: Branches/Delete/5  
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Branch branch = db.Branches.Find(id);
            db.Branches.Remove(branch.Id);
            return RedirectToAction("Index");
        }
        private bool GetLoggedInUserId(out int loggedUserId)
        {
            var identity = User.Identity as ClaimsIdentity;
            var userDateId = identity.FindFirst(c => c.Type == ClaimTypes.UserData).Value;

            return int.TryParse(userDateId, out loggedUserId);
        }
    }
}
