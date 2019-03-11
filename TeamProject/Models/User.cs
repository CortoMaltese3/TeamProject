namespace TeamProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;
    public partial class User
    {
        public User()
        {
            Booking = new HashSet<Booking>();
            Branch = new HashSet<Branch>();
            Review = new HashSet<Review>();
            Roles = new List<Role>();
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

        public virtual List<Role> Roles { get; set; }

        public string AllRoles
        {
            get
            {
                var orderedRoled = Roles
                    .OrderBy(r => r.Description)
                    .Select(r => r.Description);
                return string.Join(", ", orderedRoled);

            }
        }
    }
}
