using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;

namespace TeamProject.Models
{
    public class BookingReport
    {
        public int BookingDayNo { get; set; }

        public string BookingDay { get; set; }

        public int CountOfBookings { get; set; }


        public BookingReport(ProjectDbContext dbContext)
        {
            
        }
    }
}