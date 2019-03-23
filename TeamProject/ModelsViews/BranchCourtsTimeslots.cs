using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TeamProject.Models;
namespace TeamProject.ModelsViews
{
    public class BranchCourtsTimeslots
    {
        public int BranchId { get; set; }
        public int CourtId { get; set; }
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
        public IEnumerable<Court> Courts { get; set; }

        public IEnumerable<ModelsViews.TimeslotApiView> TimeslotApiViews { get; set; }

    }
}