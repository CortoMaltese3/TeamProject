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
        public IEnumerable<IGrouping<string, GroupData>> GroupData { get; set; }

    }
    public class GroupData
    {
        [DisplayFormat(DataFormatString = "{0:dddd dd/MM/yyyy}")]
        public string Day { get; set; }
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}")]
        public string Time { get; set; }
        public User User { get; set; }
        public int Duration { get; set; }
    }
}