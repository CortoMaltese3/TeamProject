namespace TeamProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Review
    {
        public int Id { get; set; }

        public int CourtId { get; set; }

        public int UserId { get; set; }

        public int Rating { get; set; }

        [StringLength(250)]
        public string Comment { get; set; }

        public DateTime CommentAt { get; set; }

        public virtual Court Court { get; set; }

        public virtual User User { get; set; }
    }
}
