using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeamProject.Models
{
    public class FacilityFormModel
    {
        public int BranchId { get; set; }
        public IList<int> SelectedFacilities { get; set; }
        public IList<SelectListItem> AvailableFacilities { get; set; }

        public FacilityFormModel()
        {
            SelectedFacilities = new List<int>();
            AvailableFacilities = new List<SelectListItem>();
        }
    }
}