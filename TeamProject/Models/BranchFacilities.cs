using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamProject.Models
{
    public class BranchFacilities
    {
        public int BranchId { get; set; }
        public int FacilityId { get; set; }
        public Branch Branch { get; set; }
        public Facility Facility { get; set; }


    }
}