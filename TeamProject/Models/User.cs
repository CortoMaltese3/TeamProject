using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using TeamProject.Dal;

namespace TeamProject.Models
{

    public class User
    {
        public User()
        {
            Booking = new List<Booking>();
            Branch = new List<Branch>();
            Review = new List<Review>();
            Roles = new List<Role>();
        }

        public int Id { get; set; }

        [TableField]
        [Required]
        [DisplayName("First Name")]
        [StringLength(50)]
        public string Firstname { get; set; }

        [TableField]
        [Required]
        [DisplayName("Last Name")]
        [StringLength(50)]
        public string Lastname { get; set; }

        [TableField]
        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [TableField(ExcludeFromUpdate = true)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password")]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }

        public ICollection<Booking> Booking { get; set; }

        public ICollection<Branch> Branch { get; set; }

        public ICollection<Review> Review { get; set; }

        public List<Role> Roles { get; set; }

        #region NotMapped
        [DisplayName("User Roles")]
        public string AllRoles => string.Join(", ", Roles.OrderBy(r => r.Description).Select(r => r.Description));
        public string UserName => Firstname + " " + Lastname;
        #endregion
    }
}
