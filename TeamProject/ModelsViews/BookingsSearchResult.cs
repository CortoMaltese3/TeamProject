using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TeamProject.Models;
namespace TeamProject.ModelsViews
{
    public class BookingsSearchResult
    {
        public int BranchId { get; set; }
        public int CourtId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public IEnumerable<Court> Courts { get; set; }

        public IEnumerable<ModelsViews.TimeslotApiView> TimeslotApiViews { get; set; }
        public IEnumerable<Models.Booking> Bookings { get; set; }
        public IEnumerable<IGrouping<string, BookingInfoByDay>> GroupData { get; set; }

    }
    public class BookingInfoByDay
    {
        public Booking Booking { get; set; }

        public string Day { get => Booking.BookedAt.ToLongDateString(); }

        public string Time { get => Booking.BookedAt.ToString("HH:mm"); }
    }
}