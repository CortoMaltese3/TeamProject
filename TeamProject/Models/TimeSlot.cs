namespace TeamProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TimeSlot")]
    public partial class TimeSlot
    {
        public int ID { get; set; }

        public int CourtID { get; set; }

        public int Day { get; set; }

        public TimeSpan Hour { get; set; }

        public int Duration { get; set; }

        public virtual Court Court { get; set; }
    }
}
