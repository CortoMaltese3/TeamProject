using Microsoft.AspNet.Identity;
using TeamProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeamProject.Controllers
{
    public class LoginController : Controller
    {
        private ProjectDbContext db = new ProjectDbContext();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(string email, string password)
        {
            UserManager manager = new UserManager(db);
            var loggedinuser = manager.Login(email, password);

            if (loggedinuser != null)
            {
                Session["user"] = loggedinuser;

                Session["Admin"] = loggedinuser.Roles.Any(x => x.Description.Equals("Admin"))? "Admin":"";

                Session["Owner"] = loggedinuser.Roles.Any(o => o.Description.Equals("Owner")) ? "Owner" : "" ;

                ViewBag.Name = loggedinuser.Firstname;
                return RedirectToAction("index", "home");
            }
            else
            {
                return RedirectToAction("index");
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}
