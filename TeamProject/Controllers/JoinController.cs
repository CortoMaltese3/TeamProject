using System.Net;
using System.Web.Mvc;
using TeamProject.Models;
using System.Linq;
using System;
using TeamProject.Dal;
using System.Collections.Generic;

namespace TeamProject.Controllers
{
    public class JoinController : Controller
    {
        private TeamProjectApp app = new TeamProjectApp();

        public ActionResult Join()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Join(User user)
        {
            (bool valid, string field, string error) = app.AddUser(user, ModelState.IsValid);

            if (!valid)
            {
                ModelState.AddModelError(field, error);
                return View(user);
            }

            return RedirectToAction("Index","Home");
        }
          
    }
}
