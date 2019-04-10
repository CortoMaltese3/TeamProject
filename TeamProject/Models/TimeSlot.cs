using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamProject.Models
{

    public  class TimeSlot
    {
        public int Id { get; set; }

        public int CourtId { get; set; }

        public int Day { get; set; }

        [DataType(DataType.Time)]
        public TimeSpan Hour { get; set; }

        public int Duration { get; set; }

        public  Court Court { get; set; }
    }
}
