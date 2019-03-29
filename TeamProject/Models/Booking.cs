using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamProject.Models
{

    public class Booking
    {
        public int Id { get; set; }

        public int CourtId { get; set; }

        public int UserId { get; set; }

        [DisplayName("Booked Date/Time")]
        [DisplayFormat(DataFormatString = "{0:dddd dd/MM/yyyy HH:mm}")]
        public DateTime BookedAt { get; set; }

        public int Duration { get; set; }

        public string BookKey { get; set; }

        public Court Court { get; set; }

        public User User { get; set; }
    }
}
