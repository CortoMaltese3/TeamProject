using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeamProject.Models;

namespace TeamProject.Areas.Admin.Controllers
{
    public class BranchFacilitiesController : Controller
    {
        private ProjectDbContext db = new ProjectDbContext();
        [Authorize(Roles = "Owner,Admin")]
        public ActionResult ChooseFacilities(int id)
        {
            var facilityFormModel = new FacilityFormModel()
            {
                AvailableFacilities = db.BranchFacilities.GetFacilities(id),
                BranchId = id
            };

            return View(facilityFormModel);
        }

        public ActionResult AddFacilities(FacilityFormModel facilityFormModel)
        {
            if (!ModelState.IsValid)
            {
                facilityFormModel.AvailableFacilities = db.BranchFacilities.GetFacilities(facilityFormModel.BranchId);
                return View(facilityFormModel);
            }

            // remove all branch facilities
            db.BranchFacilities.Remove(facilityFormModel.BranchId);

            // add selected facilities
            var facilities = facilityFormModel.SelectedFacilities;
            foreach (var item in facilities)
            {
                db.BranchFacilities.Add(new BranchFacilities() { BranchId = facilityFormModel.BranchId, FacilityId = item });
            }

            return RedirectToAction("Index", "Branches");
        }

    }
}