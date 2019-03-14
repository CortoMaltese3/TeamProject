using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamProject.ModelsViews
{
    public class TimeslotApiView
    {
        public int Id { get; set; }

        public int CourtId { get; set; }

        public int Day { get; set; }

        public TimeSpan Hour { get; set; }

        public int Duration { get; set; }

        public int BookingId{ get; set; }

    }
}