using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeamProject.Dal;

namespace TeamProject.Models
{
    public class BranchFacilities : IEquatable<BranchFacilities>
    {
        [TableField(PrimaryKey = true)]
        public int BranchId { get; set; }

        [TableField(PrimaryKey = true)]
        public int FacilityId { get; set; }

        public Branch Branch { get; set; }
        public Facility Facility { get; set; }

        // used for union with available facilitie in BranchFacilities Controller.ChooseFacilities Method
        public bool Equals(BranchFacilities other)
        {
            if (other == null)
            {
                return false;
            }

            return FacilityId == other.FacilityId;
        }
        public override bool Equals(object obj) => Equals(obj as BranchFacilities);
        public override int GetHashCode() => FacilityId.GetHashCode();
    }

}