using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace TeamProject.Models
{


    public class Court
    {

        public int Id { get; set; }

        public int BranchId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Court Name")]
        public string Name { get; set; }


        [StringLength(500)]
        public string ImageCourt { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        public string Description { get; set; }

        public int MaxPlayers { get; set; }

        public decimal Price { get; set; }

        public ICollection<Booking> Booking { get; set; }


        public Branch Branch { get; set; }

        public ICollection<Review> Review { get; set; }

        public ICollection<TimeSlot> TimeSlot { get; set; }
    }
}
