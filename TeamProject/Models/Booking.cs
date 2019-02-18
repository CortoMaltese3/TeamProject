namespace TeamProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Booking")]
    public partial class Booking
    {
        public int Id { get; set; }

        public int CourtId { get; set; }

        public int UserId { get; set; }

        public DateTime BookedAt { get; set; }

        public int Duration { get; set; }

        public virtual Court Court { get; set; }

        public virtual User User { get; set; }
    }
}
