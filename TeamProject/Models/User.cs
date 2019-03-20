using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Linq;

namespace TeamProject.Models
{

    public partial class User
    {
        public User()
        {
            Booking = new List<Booking>();
            Branch = new List<Branch>();
            Review = new List<Review>();
            Roles = new List<Role>();
        }
        [Key]
        public int Id { get; set; }


        [DisplayName ("Firstname (optional)")]
        [StringLength(50)]
        public string Firstname { get; set; }

        [DisplayName ("Lastname (optional)")]
        [StringLength(50)]
        public string Lastname { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        //[Required(ErrorMessage = "Password is required")]
        //[StringLength(50, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        //[Required(ErrorMessage = "Confirm Password is required")]
        //[StringLength(50, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }

        public virtual ICollection<Booking> Booking { get; set; }

        public virtual ICollection<Branch> Branch { get; set; }

        public virtual ICollection<Review> Review { get; set; }

        public virtual List<Role> Roles { get; set; }

        #region NotMapped
        [DisplayName("User Roles")]
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
        public string UserName
        {
            get
            {
                return Firstname +" " +Lastname;
            }
        }
        public bool IsInRole(string role)
        {
            return Roles
                .Count(r => r.Description.Equals(role, StringComparison.InvariantCultureIgnoreCase)) == 1;
        }
        #endregion
    }
}
