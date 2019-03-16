using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TeamProject.Models;

namespace TeamProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CourtsController : Controller
    {
        private ProjectDbContext db = new ProjectDbContext();

        // GET: Courts
        public ActionResult Index()
        {
            var court = db.Courts.Get();//.Include(c => c.Branch);
            ViewBag.BranchName = new SelectList (db.Branches.Get(),"Id","Name");

            return View(court.ToList());
        }

        // GET: Courts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Court court = db.Courts.Find(id??0);
            if (court == null)
            {
                return HttpNotFound();
            }
            return View(court);
        }

        // GET: Courts/Create
        public ActionResult Create()
        {
            ViewBag.BranchId = new SelectList(db.Branches.Get(), "Id", "Name");
            return View();
        }

        // POST: Courts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create (Court court)
        {
            court.ImageCourt = Path.GetFileName(court.ImageFile.FileName);
            string fileName = Path.Combine(Server.MapPath("~/Images/CourtsImages/"), court.ImageCourt);
            court.ImageFile.SaveAs(fileName);

            if (ModelState.IsValid)
            {
                db.Courts.Add(court);
                return RedirectToAction("Index");
            }

            ViewBag.BranchId = new SelectList(db.Branches.Get(), "Id", "Name", court.BranchId);
            return View(court);
        }

        // GET: Courts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Court court = db.Courts.Find(id??0);
            if (court == null)
            {
                return HttpNotFound();
            }
            ViewBag.BranchId = new SelectList(db.Branches.Get(), "Id", "Name", court.BranchId);
            return View(court);
        }

        // POST: Courts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Court court, HttpPostedFileBase ImageFile)
        {
            if (ImageFile != null)
            {
                court.ImageCourt = Path.GetFileName(court.ImageFile.FileName);
                string fileName = Path.Combine(Server.MapPath("~/Images/CourtImages"), court.ImageCourt);
                court.ImageFile.SaveAs(fileName);
            }

            if (ModelState.IsValid)
            {
                db.Courts.Update(court);
                return RedirectToAction("Index");
            }
            ViewBag.BranchId = new SelectList(db.Branches.Get(), "Id", "Name", court.BranchId);
            ViewBag.Id = court.Id;
            return View(court);
        }

        // GET: Courts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Court court = db.Courts.Find(id??0);
            if (court == null)
            {
                return HttpNotFound();
            }
            return View(court);
        }

        // POST: Courts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Court court = db.Courts.Find(id);
            db.Courts.Remove(court.Id);
            return RedirectToAction("Index");
        }


    }
}
