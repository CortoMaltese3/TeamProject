using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeamProject.Models;

namespace TeamProject.Controllers
{
    
    public class HomeController : Controller
    {
        private ProjectDbContext db = new ProjectDbContext();

        // GET: Courts
        public ActionResult Index()
        {
            var court = db.Courts.Get().ToList();//.Include(c => c.Branch);
            var courtlist = new List<Court>();
            var courtcount = court.Count();
            for (var i= 0; i<6 && i<courtcount; i++)
            {
                var courtrandom = court.PickRandom();
                courtlist.Add(courtrandom);
                court.Remove(courtrandom);
            }
            return View(courtlist);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Contributors' Wall of Glory";

            return View();
        }

        // GET: Contact
        public ActionResult Contact()
        {
            ViewBag.Message = "Whether you would like to partner with us, want more information or run into an issue," +
                              "we are more than happy to hear from you!";

            return View();
        }

        //POST: Contact
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactForm contactForm)
        {

            return View();
        }
    }

    public static class EnumerableExtension
    {
        public static T PickRandom<T>(this IEnumerable<T> source)
        {
            
            return source.PickRandom(1).Single();
        }

        public static IEnumerable<T> PickRandom<T>(this IEnumerable<T> source, int count)
        {
            return source.Shuffle().Take(count);
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            return source.OrderBy(x => Guid.NewGuid());
        }
    }
}