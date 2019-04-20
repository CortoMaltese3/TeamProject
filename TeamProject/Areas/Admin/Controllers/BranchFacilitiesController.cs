using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeamProject.Dal;
using TeamProject.Models;

namespace TeamProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Owner,Admin")]
    public class BranchFacilitiesController : Controller
    {
        private ProjectDbContext db = new ProjectDbContext();
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

            // remove all branch facilities
            db.BranchFacilities.Remove(facilityFormModel.BranchId);

            // add selected facilities
            if (facilityFormModel.SelectedFacilities != null)
            {
                foreach (var item in facilityFormModel.SelectedFacilities)
                {
                    db.BranchFacilities.Add(new BranchFacilities() { BranchId = facilityFormModel.BranchId, FacilityId = item });
                }
            }

            return RedirectToAction("Index", "Branches");
        }

    }
}