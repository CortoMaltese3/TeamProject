using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TeamProject.Dal;
using TeamProject.Models;

namespace TeamProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TimeSlotsController : Controller
    {
        private ProjectDbContext db = new ProjectDbContext();

        public ProjectDbContext Db { get => db; set => db = value; }

        // GET: TimeSlots
        public ActionResult Index(int? id)
        {
            var timeSlot = Db.TimeSlots.Get().Where(t => t.CourtId == (id ?? 0));//.Include(t => t.Court);
            ViewBag.id = id;            
            ViewBag.courtName = Db.Courts.Find(id ?? 0).Name;
            ViewBag.branchId = Db.Courts.Find(id ?? 0).BranchId;            

            var timeslotApiViews = Db.TimeSlots.GetForView(id ?? 0).OrderBy(t => t.Hour).ToList();


            return View(timeslotApiViews);
        }

        // GET: TimeSlots/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeSlot timeSlot = Db.TimeSlots.Find(id ?? 0);
            if (timeSlot == null)
            {
                return HttpNotFound();
            }
            //ViewBag.id = timeSlot.CourtId;
            return View(timeSlot);
        }

        // GET: TimeSlots/Create
        public ActionResult Create(int? id)
        {
            ViewBag.CourtId = new SelectList(Db.Courts.Get().Where(c => c.Id == (id ?? 0)), "Id", "Name");
            ViewBag.id = id;
            return View();
        }

        // POST: TimeSlots/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CourtId,Day,Hour,Duration")] TimeSlot timeSlot)
        {
            if (ModelState.IsValid)
            {
                Db.TimeSlots.Add(timeSlot);
                return RedirectToAction("Index", new { id = timeSlot.CourtId });
            }

            ViewBag.CourtId = new SelectList(Db.Courts.Get(), "Id", "Name", timeSlot.CourtId);
            ViewBag.id = timeSlot.CourtId;
            return View(timeSlot);
        }

        // GET: TimeSlots/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeSlot timeSlot = Db.TimeSlots.Find(id ?? 0);
            if (timeSlot == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourtId = new SelectList(Db.Courts.Get(), "Id", "Name", timeSlot.CourtId);
            return View(timeSlot);
        }

        // POST: TimeSlots/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CourtId,Day,Hour,Duration")] TimeSlot timeSlot)
        {
            if (ModelState.IsValid)
            {
                Db.TimeSlots.Update(timeSlot);
                return RedirectToAction("Index", new { id = timeSlot.CourtId });
            }
            ViewBag.CourtId = new SelectList(Db.Courts.Get(), "Id", "Name", timeSlot.CourtId);
            return View(timeSlot);
        }

        // GET: TimeSlots/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeSlot timeSlot = Db.TimeSlots.Find(id ?? 0);
            if (timeSlot == null)
            {
                return HttpNotFound();
            }
            return View(timeSlot);
        }

        // POST: TimeSlots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TimeSlot timeSlot = Db.TimeSlots.Find(id);

            Db.TimeSlots.Remove(id);
            return RedirectToAction("Index", new { id = timeSlot.CourtId });
        }
    }
}
