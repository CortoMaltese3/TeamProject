using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeamProject.Areas.Admin.Controllers
{
    public class PanelController : Controller
    {
        // GET: Admin/Panel
        [Authorize (Roles ="Admin,Owner")]
        public ActionResult Index()
        {
            return View();
        }
    }
}