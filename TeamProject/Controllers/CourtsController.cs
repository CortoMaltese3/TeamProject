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
        private TeamProjectApp app = new TeamProjectApp();

        // GET: Courts
        public ActionResult Index(int? id)
        {
            var court = db.Courts.Get();
            if (id != null)
            {
                court = app.BranchCourts(id ?? 0);

            }
            return View(court);
        }

        [Authorize]
        public ActionResult Booking(int id)
        {
            var courtsInSameBranch = app.AllCourtsSameBranch(id).ToList();

            return View(new BookViewModel()
            {
                CourtId = id,
                Courts = courtsInSameBranch
            });
        }

        public ActionResult Confirmed(string bookKey)
        {
            var (booking, branch) = GetBookingFromKey(bookKey);

            SmtpMessageChunk.SendMessageSmtp(booking, branch, Request.Url);

            ViewBag.address = branch.Address;
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

            ViewBag.Facilities = db.Facilities.Get().Where(x => x.Branch.Any(b => b.Id == court.Branch.Id));
            return View(court);
        }

        public FileResult GetHTMLPageAsPDF(string bookKey)
        {
            var (booking, branch) = GetBookingFromKey(bookKey);
            var render = new IronPdf.HtmlToPdf();
            
            //Create a PDF Document
            var PDF = render.RenderHtmlAsPdf(
                    $@"<h2> {booking.User.UserName}, thanks for booking.</h2>
                    <br/>
                    <div> You have booked <strong> {booking.Court.Name} </strong> at <strong>{booking.BookedAt}</strong>
                    </div><br/>
                    <span> Your booking number is <strong>{ booking.BookKey}</strong></span >
                    <div style='height: 500px'>
                        <img style='height:256px' src='data:image/jpeg;base64,{booking.QrCodeImageAsBase64(Request.Url)}'>
                    </div>
                    <span> You can find the Court at {branch.Address}</span><br/><span><strong> Price:</strong> { booking.Court.Price} &euro; ");
            //return a  pdf document from a view
            var contentLength = PDF.BinaryData.Length;
            Response.AppendHeader("Content-Length", contentLength.ToString());
            Response.AppendHeader("Content-Disposition", "inline; filename=Booking_" + booking.User.UserName + ".pdf");
            return File(PDF.BinaryData, "application/pdf;");
        }

        private (Booking, Branch) GetBookingFromKey(string bookKey)
        {
            var booking = db.Bookings.Get("BookKey = @bookKey", new { bookKey }).FirstOrDefault();
            var branch = db.Branches.Get("Branch.Id = @BranchId", new { booking.Court.BranchId }).FirstOrDefault();

            return (booking, branch);
        }
    }
}