using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeamProject.Dal;
using TeamProject.Models;
using TeamProject.ModelsViews;

namespace TeamProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Owner,Admin")]
    public class BranchFacilitiesController : Controller
    {
        private TeamProjectApp app = new TeamProjectApp();

        public ActionResult ChooseFacilities(int id)
        {
            return View(new FacilityFormModel()
            {
                AvailableFacilities = app.GetAvailableFacilities(id),
                BranchId = id
            });
        }

        [HttpPost]
        public ActionResult ChooseFacilities(FacilityFormModel facilityFormModel)
        {
            if (!ModelState.IsValid)
            {
                facilityFormModel.AvailableFacilities = app.GetAvailableFacilities(facilityFormModel.BranchId);
                return View(facilityFormModel);
            }

            app.UpdateBranchFacilities(facilityFormModel);

            return RedirectToAction("Index", "Branches");
        }


    }
}