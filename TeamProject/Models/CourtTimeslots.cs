using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamProject.Models
{
    public class CourtTimeslots
    {
        public Court court { get; set; }

        public IEnumerable<ModelsViews.TimeslotApiView> timeslotApiViews { get; set; }

    }
}