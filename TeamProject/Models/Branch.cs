using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

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

        [Display(Name = "Branch Owner")]
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Branch Name")]
        public string Name { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        [StringLength(20)]
        public string City { get; set; }

        [Required]
        [StringLength(200)]
        public string Address { get; set; }

        [StringLength(200)]
        public string ZipCode { get; set; }

        public string ImageBranch { get; set; }

        public double Distance { get; set; }

        public User User { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        public ICollection<Court> Court { get; set; }

        public ICollection<Facility> Facility { get; set; }
    }
}
