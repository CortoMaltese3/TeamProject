using System.Linq;
using System.Net;
using System.Web.Mvc;
using TeamProject.Models;

namespace TeamProject.Areas.Admin.Controllers
{
    public class UserRolesController : Controller
    {
        private ProjectDbContext db = new ProjectDbContext();

        // GET: UserRoles
        public ActionResult Index()
        {
            var userRoles = db.UserRoles.Get();//.Include(u => u.User);
            return View(userRoles.ToList());
        }

        // GET: UserRoles/Details/5
        public ActionResult Details(int? id, int? roleId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRoles userRoles = db.UserRoles.Find(id ?? 0);
            if (userRoles == null)
            {
                return HttpNotFound();
            }
            return View(userRoles);
        }

        // GET: UserRoles/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users.Get(), "Id", "Firstname");
            return View();
        }

        // POST: UserRoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,Role")] UserRoles userRoles)
        {
            if (ModelState.IsValid)
            {
                db.UserRoles.Add(userRoles);
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users.Get(), "Id", "Firstname", userRoles.UserId);
            return View(userRoles);
        }

        // GET: UserRoles/Edit/5
        public ActionResult Edit(int? id, int? roleId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRoles userRoles = db.UserRoles.Get("UserId=@id And RoleId=roleId", new { id, roleId }).FirstOrDefault();
            if (userRoles == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users.Get(), "Id", "Firstname", userRoles.UserId);
            ViewBag.RoleId = new SelectList(db.Roles.Get(), "Id", "Description", userRoles.RoleId);
            return View(userRoles);
        }

        // POST: UserRoles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,Role")] UserRoles userRoles)
        {
            if (ModelState.IsValid)
            {
                db.UserRoles.Update(userRoles);
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users.Get(), "Id", "Firstname", userRoles.UserId);
            ViewBag.RoleId = new SelectList(db.Roles.Get(), "Id", "Description", userRoles.RoleId);
            return View(userRoles);
        }

        // GET: UserRoles/Delete/5
        public ActionResult Delete(int? id, int? roleId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRoles userRoles = db.UserRoles.Find(id ?? 0);
            if (userRoles == null)
            {
                return HttpNotFound();
            }
            return View(userRoles);
        }

        // POST: UserRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int? roleId)
        {
            db.UserRoles.Remove(id);
            return RedirectToAction("Index");
        }

    }
}
