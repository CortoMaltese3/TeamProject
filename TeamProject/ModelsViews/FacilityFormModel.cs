using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeamProject.ModelsViews
{
    public class FacilityFormModel
    {
        public int BranchId { get; set; }
        public IEnumerable<int> SelectedFacilities { get; set; }
        public IEnumerable<SelectListItem> AvailableFacilities { get; set; }

    }
}