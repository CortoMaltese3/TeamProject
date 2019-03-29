using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamProject.Models
{


    public class UserRoles
    {
        [Required]
        [Key]
        public int UserId { get; set; }
        [Required]
        public int RoleId { get; set; }

        public Role Role { get; set; }

        public User User { get; set; }
    }
}
