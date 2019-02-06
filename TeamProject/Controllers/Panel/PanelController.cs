using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace TeamProject.Controllers.Panel
{
    public class PanelController : Controller
    {
        [AuthorizeAttribute]
        public IActionResult Index()
        {
            return View();
        }
    }
}