using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamProject.Models
{

    public class Role
    {
        public Role()
        {
            IsEnabled = true;
        }

        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        #region NotMapped
        public bool IsNew { get; set; }
        public bool IsEnabled { get; set; }
        #endregion
    }
}