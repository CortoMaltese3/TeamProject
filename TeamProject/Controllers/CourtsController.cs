using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using IronPdf;
using System.Web.Mvc;
using TeamProject.Dal;
using TeamProject.Models;
using TeamProject.ModelsViews;

namespace TeamProject.Controllers
{
    public class CourtsController : Controller
    {
        private ProjectDbContext db = new ProjectDbContext();
        // GET: Courts
        public ActionResult Index(int? id)
        {
            var court = db.Courts.Get().ToList();
            if(id != null)
            {
                court = db.Courts.Get().Where(x => x.BranchId == id).ToList();
               
            }
            //ViewBag.BranchName = new SelectList(db.Branches.Get(), "Id", "Name");
            //ViewBag.Id = id;
            return View(court);
        }
        [Authorize]
        public ActionResult Booking(int id)
        {
            var courtsInSameBranch = db.Courts.AllCourtsSameBranch(id).ToList();

            var bookViewModel = new BookViewModel()
            {
                CourtId = id,
                Courts = courtsInSameBranch
            };
            return View(bookViewModel);
        }


        public ActionResult Confirmed(string BookKey)
        {
            var booking = db.Bookings.Get().Where(x => x.BookKey == BookKey).FirstOrDefault();
            var branch = db.Branches.Get().Where(x => x.Id == booking.Court.BranchId).FirstOrDefault();
            ViewBag.address = branch.Address;
            ViewBag.city = branch.City;

            SmtpMessageChunk.SendMessageSmtp(booking, branch);
            return View(booking);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Court court = db.Courts.Find(id ?? 0);
            if (court == null)
            {
                return HttpNotFound();
            }

            ViewBag.Facilities = db.Facilities.Get().Where(x => x.Branch.Any(b=>b.Id == court.Branch.Id));
            return View(court);
        }

        public FileResult GetHTMLPageAsPDF(string BookKey)
        {
            var booking = db.Bookings.Get().Where(x => x.BookKey == BookKey).FirstOrDefault();
            var branch = db.Branches.Get().Where(x => x.Id == booking.Court.BranchId).FirstOrDefault();
            var render = new IronPdf.HtmlToPdf();
            //Create a PDF Document
            var PDF = render.RenderHtmlAsPdf(
                    $@"<h2> {booking.User.UserName} Thanks for booking.</h2>
                    <br/>
                    <div> You have book <strong> { booking.Court.Name} </strong> at <strong>{ booking.BookedAt}</strong>
                    </div><br/>
                    <span> Your booking number is <strong>{ booking.BookKey}</strong></span >
                    <br/><br/><span> You can find the Court at { branch.Address}</span><br/><span><strong> Price:</strong> { booking.Court.Price} &euro; ");
            //return a  pdf document from a view
            var contentLength = PDF.BinaryData.Length;
            Response.AppendHeader("Content-Length", contentLength.ToString());
            Response.AppendHeader("Content-Disposition", "inline; filename=Booking_" + booking.User.UserName + ".pdf");
            return File(PDF.BinaryData, "application/pdf;");
        }
    }
}