namespace TeamProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    public partial class Facility
    {        
        public Facility()
        {
            Branch = new List<Branch>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Description { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        public string ImageFacility { get; set; }

        public virtual ICollection<Branch> Branch { get; set; }
    }
}
