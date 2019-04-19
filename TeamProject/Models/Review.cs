using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeamProject.Dal;

namespace TeamProject.Models
{


    public class Review
    {
        public int Id { get; set; }

        [TableField]
        public int CourtId { get; set; }

        [TableField]
        public int UserId { get; set; }

        [TableField]
        public int Rating { get; set; }

        [TableField]
        [StringLength(250)]
        public string Comment { get; set; }

        [TableField]
        public DateTime CommentAt { get; set; }

        public Court Court { get; set; }

        public User User { get; set; }
    }
}
