using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using TeamProject.Dal;

namespace TeamProject.Models
{

    public class Facility 
    {
        public Facility()
        {
            Branch = new List<Branch>();
        }

        public int Id { get; set; }

        [TableField]
        [Required]
        [StringLength(20)]
        public string Description { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        [TableField]
        public string ImageFacility { get; set; }

        public ICollection<Branch> Branch { get; set; }

    }
}
