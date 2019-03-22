using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeamProject.Models
{
    public class BranchFacilities
    {
        public int BranchId { get; set; }
        public int FacilityId { get; set; }
        public Branch Branch { get; set; }
        public Facility Facility { get; set; }

        public IList<int> SelectedFacilities { get; set; }
        public IList<Facility> AvailableFacilities { get; set; }

        public BranchFacilities()
        {
            SelectedFacilities = new List<int>();
            AvailableFacilities = new List<Facility>();
        }
    }

}