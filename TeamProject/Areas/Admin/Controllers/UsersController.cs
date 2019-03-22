using System.Net;
using System.Web.Mvc;
using TeamProject.Models;
using System.Linq;
using System;

namespace TeamProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
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

        [AllowAnonymous]
        public ActionResult Join()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Join(User user)
        {
            return CreateUser(user, 
                CreateValidations,
                () => Redirect("~/Home/Index"));
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            return CreateUser(user,
                CreateValidations,
                () => RedirectToAction("Index"));
        }

        private ActionResult CreateUser(User user, Func<User, bool> validations, Func<ActionResult> redirectTo)
        {

            if (!validations(user))
            {
                return View(user);
            }

            if (!db.Users.AddSimpleUser(user))
            {
                ModelState.AddModelError("UserName", "Failed to create new user.");
                return View(user);
            }

            return redirectTo();

        }
        private bool CreateValidations(User user)
        {
            //UserManager manager = new UserManager(db);
            if (db.Users.EmailExists(user.Email))
            {
                ModelState.AddModelError("Email", "E-mail already taken! Choose an other E-mail!");
                return false;
            }
            if (user.Password == null)
            {
                ModelState.AddModelError("Password", "Password is required");
                return false;
            }
            return ModelState.IsValid;
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

            // find roles not enabled for selected user
            var roles = db
                .Roles
                .Get()
                .Where(r => !user.Roles.Any(ur => ur.Id == r.Id))
                .Select(r => new Role()
                {
                    Id = r.Id,
                    Description = r.Description,
                    IsEnabled = false,
                    IsNew = true
                });

            // add found roles to user
            user.Roles.AddRange(roles);
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
            if (ModelState.IsValid)
            {
                // remove existing roles that got disabled 
                user.Roles
                    .Where(r => !r.IsNew && !r.IsEnabled)
                    .ToList()
                    .ForEach((role) =>
                    {
                        db.UserRoles.Remove(user.Id, role.Id);
                    });

                // add new roles
                user.Roles
                    .Where(r => r.IsNew && r.IsEnabled)
                    .ToList()
                    .ForEach((role) =>
                        {
                            db.UserRoles.Add(new UserRoles() { UserId = user.Id, RoleId = role.Id });
                        });

                //update user
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
