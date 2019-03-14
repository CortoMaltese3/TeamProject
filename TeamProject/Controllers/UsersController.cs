using System.Net;
using System.Web.Mvc;
using TeamProject.Models;
using System.Linq;

namespace TeamProject.Controllers
{
    public class UsersController : Controller
    {
        private ProjectDbContext db = new ProjectDbContext();

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

                return RedirectToAction("Index","Home");
            }

            return View(user);
        }
               
    }
}
