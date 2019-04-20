using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeamProject.Dal;

namespace TeamProject.Models
{
    public class BranchFacilities
    {
        [TableField(PrimaryKey = true)]
        public int BranchId { get; set; }

        [TableField(PrimaryKey = true)]
        public int FacilityId { get; set; }

        public Branch Branch { get; set; }
        public Facility Facility { get; set; }

    }

}