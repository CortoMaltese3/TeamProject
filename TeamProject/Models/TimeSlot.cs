using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeamProject.Dal;

namespace TeamProject.Models
{

    public  class TimeSlot
    {
        public int Id { get; set; }

        [TableField]
        public int CourtId { get; set; }

        [TableField]
        public int Day { get; set; }

        [TableField]
        [DataType(DataType.Time)]
        public TimeSpan Hour { get; set; }

        [TableField]
        public int Duration { get; set; }

        public  Court Court { get; set; }
    }
}
