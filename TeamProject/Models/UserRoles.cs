using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeamProject.Dal;

namespace TeamProject.Models
{


    public class UserRoles
    {
        [TableKey("UserId,RoleId")]
        [Required]
        public int UserId { get; set; }
        [Required]
        public int RoleId { get; set; }

        public Role Role { get; set; }

        public User User { get; set; }
    }
}
