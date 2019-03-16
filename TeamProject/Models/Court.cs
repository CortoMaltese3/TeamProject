namespace TeamProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    public partial class Court
    {

        public int Id { get; set; }

        public int BranchId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Court Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        public string ImageCourt { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        public int MaxPlayers { get; set; }

        //[Required(ErrorMessage = "Price is required")]
        //[Range(0.00, 999.99, ErrorMessage = "Price must be less than 1000.00")]
        //[DisplayName("Price ($)")]
        //[DataType(DataType.Custom,ErrorMessage = "Price in Euros")]
        public decimal Price { get; set; }

        public virtual ICollection<Booking> Booking { get; set; }

        
        public virtual Branch Branch { get; set; }

        public virtual ICollection<Review> Review { get; set; }

        public virtual ICollection<TimeSlot> TimeSlot { get; set; }
    }
}
