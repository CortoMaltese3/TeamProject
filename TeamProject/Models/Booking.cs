namespace TeamProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Booking
    {
        public int Id { get; set; }

        public int CourtId { get; set; }

        public int UserId { get; set; }

        [DisplayName("Booked Date/Time")]
        [DisplayFormat(DataFormatString ="{0:dddd dd/MM/yyyy HH:mm}")]
        public DateTime BookedAt { get; set; }

        public int Duration { get; set; }

        public string BookKey { get; set; }

        public virtual Court Court { get; set; }

        public virtual User User { get; set; }
    }
}
