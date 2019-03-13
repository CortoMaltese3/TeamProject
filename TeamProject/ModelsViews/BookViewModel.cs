using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TeamProject.Models;

namespace TeamProject.ModelsViews
{
    public class BookViewModel
    {
        public Court Court { get; set; }
        public IEnumerable<TimeSlot> TimeSlots { get; set; }
    }
}