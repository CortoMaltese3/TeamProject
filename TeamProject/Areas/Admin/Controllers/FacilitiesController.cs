using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TeamProject.Models;

namespace TeamProject.Areas.Admin.Controllers
{
    
    public class FacilitiesController : Controller
    {

        private ProjectDbContext db = new ProjectDbContext();

        // GET: Facilities
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var facility = db.Facilities.Get();            
            return View(facility.ToList());
        }

        // GET: Facilities/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facility facility = db.Facilities.Find(id??0);            
            if (facility == null)
            {
                return RedirectToAction("Index");
                //return HttpNotFound();
            }
            return View(facility);
        }

        [Authorize(Roles = "Owner,Admin")]
        public ActionResult ChooseFacilities(int id)
        {
            var facilities = db.Facilities.Get().ToList();
            var branchfacilities = new BranchFacilities();
            branchfacilities.BranchId = id;

            return View(facilities);
        }

        // GET: Facilities/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create(int? id)
        {
            ViewBag.FacilityId = new SelectList(db.Facilities.Get().Where(branch => branch.Id == (id ?? 0)), "Id", "Description");           
            return View();
        }

        // POST: Facilities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(Facility facility)
        {
            if (facility.ImageFile == null)
            {
                facility.ImageFacility = "na_image.jpg";
            }
            else
            {
                facility.ImageFacility = Path.GetFileName(facility.ImageFile.FileName);
                string fileName = Path.Combine(Server.MapPath("~/Images/Facilities/"), facility.ImageFacility);
                facility.ImageFile.SaveAs(fileName);
            }
            if (ModelState.IsValid)
            {
                db.Facilities.Add(facility);               
                return RedirectToAction("Index");
            }

            ViewBag.FacilityId = new SelectList(db.Facilities.Get(), "Id", "Description", facility.Id );          

            return View(facility);
        }

        // GET: Facilities/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facility facility = db.Facilities.Find(id??0);
            if (facility == null)
            {
                return HttpNotFound();
            }
            return View(facility);
        }

        // POST: Facilities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Facility facility, HttpPostedFileBase ImageFile)
        {
            if (ImageFile != null)
            {
                facility.ImageFacility = Path.GetFileName(facility.ImageFile.FileName);
                string fileName = Path.Combine(Server.MapPath("~/Images/Facilities/"), facility.ImageFacility);
                facility.ImageFile.SaveAs(fileName);
            }

            if (ModelState.IsValid)
            {
                db.Facilities.Update(facility);               
                return RedirectToAction("Index");
            }
            return View(facility);
        }

        // GET: Facilities/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facility facility = db.Facilities.Find(id??0);
            if (facility == null)
            {
                return HttpNotFound();
            }
            return View(facility);
        }

        // POST: Facilities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Facility facility = db.Facilities.Find(id);
            db.Facilities.Remove(facility.Id);            
            return RedirectToAction("Index");
        }
    }
}
