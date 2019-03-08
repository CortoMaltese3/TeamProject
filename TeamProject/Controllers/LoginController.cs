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
        public ActionResult Login(string Username, string Password)
        {
            //UserManager manager = new UserManager(db);
            ////var loggedInUser = manager.Login(Username, Password);

            //if (loggedInUser != null)
            //{
            //    Session["user"] = loggedInUser;
            //    return View("Success", loggedInUser);
            //}
            //else
            //{
                return RedirectToAction("Index");
            //}
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
