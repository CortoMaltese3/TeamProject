namespace TeamProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        public User()
        {
            Booking = new HashSet<Booking>();
            Branch = new HashSet<Branch>();
            Review = new HashSet<Review>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Firstname { get; set; }

        [Required]
        [StringLength(50)]
        public string Lastname { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        public virtual ICollection<Booking> Booking { get; set; }

        public virtual ICollection<Branch> Branch { get; set; }

        public virtual ICollection<Review> Review { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}
