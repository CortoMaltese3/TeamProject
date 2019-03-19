namespace TeamProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TimeSlot
    {
        public int Id { get; set; }

        public int CourtId { get; set; }

        public int Day { get; set; }

        public string DayName
        {
            get
            {
                string[] DayNames = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
                return DayNames[Day - 1];
            }
        }

        [DataType(DataType.Time)]
        public TimeSpan Hour { get; set; }

        public int Duration { get; set; }

        public virtual Court Court { get; set; }
    }
}
