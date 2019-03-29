using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace TeamProject.Models
{

    public class Facility : IEquatable<Facility>
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

        public ICollection<Branch> Branch { get; set; }

        public bool Equals(Facility other)
        {
            return other != null &&
                other.Id == Id;
        }
    }
}
