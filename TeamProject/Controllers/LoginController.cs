using Microsoft.AspNet.Identity;
using TeamProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeamProject.Dal;
using TeamProject.Managers;

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
        public ActionResult Login(string email, string password,string returnurl)
        {
            UserManager manager = new UserManager(db);
            var loggedInUser = manager.Login(email, password);

            string decodeurl = null;
            if (!string.IsNullOrEmpty(returnurl))
                decodeurl = Server.UrlDecode(returnurl);
            

            if (loggedInUser != null)
            {
                Session["user"] = loggedInUser;

                ViewBag.Name = loggedInUser.Firstname;
                return Redirect(decodeurl?? "/home/index");
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
            Request.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}
