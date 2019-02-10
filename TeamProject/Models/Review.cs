namespace TeamProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Review")]
    public partial class Review
    {
        public int ID { get; set; }

        public int BranchID { get; set; }

        public int UserID { get; set; }

        public int Rating { get; set; }

        [StringLength(250)]
        public string Comment { get; set; }

        public DateTime CommentAt { get; set; }

        public virtual Branch Branch { get; set; }

        public virtual User User { get; set; }
    }
}
