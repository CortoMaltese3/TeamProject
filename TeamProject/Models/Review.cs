using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamProject.Models
{


    public class Review
    {
        public int Id { get; set; }

        public int CourtId { get; set; }

        public int UserId { get; set; }

        public int Rating { get; set; }

        [StringLength(250)]
        public string Comment { get; set; }

        public DateTime CommentAt { get; set; }

        public Court Court { get; set; }

        public User User { get; set; }
    }
}
