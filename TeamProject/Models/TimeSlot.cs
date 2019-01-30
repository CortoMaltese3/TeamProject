using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamProject.Models
{
    public class TimeSlot
    {
        public string Pitch { get; set; }
        public string Time { get; set; }
        public bool IsBooked { get; set; }
    }
}