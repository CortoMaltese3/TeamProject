using System.Net;
using System.Web.Mvc;
using TeamProject.Models;
using System.Linq;

namespace TeamProject.Areas.Admin.Controllers
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
            var user = db.Users.Find(id ?? 0);
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
        public ActionResult Create(User user)
        {
            //UserManager manager = new UserManager(db);
            var CheckEmail = db.Users.Get("Email=@Email", new { Email = user.Email }).Count();
            if (CheckEmail > 0)
            {
                ModelState.AddModelError("Email", "E-mail already taken! Choose an other E-mail!");
            }

            if (ModelState.IsValid)
            {
                //adding the new user in db!
                var Newuser = db.Users.Add(user);

                //finding the role id for type "user"
                var role = db.Roles.Get("Description=@Description", new { Description = "User" }).FirstOrDefault();
                //adding the role id with user id in the connection table.
                var UserRole = new UserRoles();
                UserRole.UserId = Newuser.Id;
                UserRole.RoleId = role.Id;
                db.UserRoles.Add(UserRole);

                return RedirectToAction("Index", "Home");
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
            User user = db.Users.Find(id ?? 0);
            if (user == null)
            {
                return HttpNotFound();
            }
            var roles = db.Roles.Get().Where(r => !user.Roles.Any(ur => ur.Id == r.Id));
            roles.ToList().ForEach(r => r.IsNew = true);

            user.Roles.AddRange(roles);

            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                var rolesToRemove = user.Roles.Where(r => r.IsNew == false && r.Action == "remove");/// r => r.RemoveRole == true);

                if (rolesToRemove.Count() != 0)
                {
                    foreach (var role in rolesToRemove)
                    {
                        db.UserRoles.Remove(user.Id, role.Id);
                    }
                }

                var rolesToAdd = user.Roles.Where(r => r.IsNew == true && r.Action == "add");/// r => r.RemoveRole == true);

                if (rolesToAdd.Count() != 0)
                {
                    foreach (var role in rolesToAdd)
                    {
                        db.UserRoles.Add(new UserRoles() { UserId=user.Id, RoleId=role.Id });
                    }
                }

                db.Users.Update(user);
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
            var user = db.Users.Find(id ?? 0);
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
            return RedirectToAction("Index");
        }


    }
}
