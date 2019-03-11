using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace TeamProject.Models
{


    public class Role
    {
        public Role()
        {

        }

        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        public bool AddRole { get; set; }
        public bool RemoveRole { get; set; }

    }
}