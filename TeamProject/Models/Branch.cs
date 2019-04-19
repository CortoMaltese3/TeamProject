using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using TeamProject.Dal;

namespace TeamProject.Models
{

    public class Branch
    {
        public Branch()
        {
            Court = new List<Court>();
            Facility = new List<Facility>();
        }

        public int Id { get; set; }

        [TableField]
        [Display(Name = "Branch Owner")]
        public int UserId { get; set; }

        [TableField]
        [Required]
        [StringLength(50)]
        [Display(Name = "Branch Name")]
        public string Name { get; set; }

        [TableField]
        public double Longitude { get; set; }

        [TableField]
        public double Latitude { get; set; }

        [TableField]
        [StringLength(20)]
        public string City { get; set; }

        [TableField]
        [Required]
        [StringLength(200)]
        public string Address { get; set; }

        [TableField]
        [StringLength(200)]
        public string ZipCode { get; set; }

        [TableField]
        public string ImageBranch { get; set; }

        public double Distance { get; set; }

        public User User { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        public ICollection<Court> Court { get; set; }

        public ICollection<Facility> Facility { get; set; }
    }
}
