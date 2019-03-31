using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TeamProject.Models;

namespace TeamProject.Areas.Admin.ViewModels
{
    public class ReportView
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IEnumerable<BookingReport> BookingReport { get; set; }
        public string Labels { get => string.Join(",", BookingReport.OrderBy(b => b.BookingDayNo).Select(b => "\"" + b.BookingDay + "\"")); }
        public string Data { get => string.Join(",", BookingReport.OrderBy(b => b.BookingDayNo).Select(b => b.CountOfBookings)); }
    }
}