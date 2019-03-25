using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamProject.Models
{
    public class CourtTimeslots
    {
        public Court Court { get; set; }

        public IEnumerable<ModelsViews.TimeslotApiView> TimeslotApiViews { get; set; }

    }
}