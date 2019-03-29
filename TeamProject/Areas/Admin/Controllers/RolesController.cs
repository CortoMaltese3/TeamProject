using System.Linq;
using System.Net;
using System.Web.Mvc;
using TeamProject.Dal;
using TeamProject.Models;

namespace TeamProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private ProjectDbContext db = new ProjectDbContext();


        // GET: Roles
        public ActionResult Index()
        {
         
            var roles = db.Roles.Get().ToList();//.Include(b => b.User); 
            
            return View(roles);
        }

        // GET: Roles/Create
        public ActionResult Create()
        {
           
            return View();
        }

        // POST: roles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Role role)
        {
            if (ModelState.IsValid)
            {
                db.Roles.Add(role);
                return RedirectToAction("Index");
            }           
            return View(role);
        }

        // GET: roles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = db.Roles.Find(id ?? 0);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: roles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Role role)
        {
            if (ModelState.IsValid)
            {
                db.Roles.Update(role);
                return RedirectToAction("Index");
            }
            
            return View(role);
        }

        // GET: roles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = db.Roles.Find(id ?? 0);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Role role = db.Roles.Find(id);
            db.Roles.Remove(role.Id);
            return RedirectToAction("Index");
        }

    }
}
