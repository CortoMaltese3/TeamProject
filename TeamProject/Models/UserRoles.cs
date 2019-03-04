namespace TeamProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserRoles
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int RoleId { get; set; }

        public Role Role { get; set; }

        public User User { get; set; }
    }
}
