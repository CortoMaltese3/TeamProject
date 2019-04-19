using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using TeamProject.Dal;

namespace TeamProject.Models
{


    public class Court
    {
        public int Id { get; set; }

        [TableField]
        public int BranchId { get; set; }

        [TableField]
        [Required]
        [StringLength(50)]
        [Display(Name = "Court Name")]
        public string Name { get; set; }

        [TableField]
        [StringLength(500)]
        public string ImageCourt { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        [TableField]
        public string Description { get; set; }

        [TableField]
        public int MaxPlayers { get; set; }

        [TableField]
        public decimal Price { get; set; }

        public ICollection<Booking> Booking { get; set; }

        public Branch Branch { get; set; }

        public ICollection<Review> Review { get; set; }

        public ICollection<TimeSlot> TimeSlot { get; set; }
    }
}
