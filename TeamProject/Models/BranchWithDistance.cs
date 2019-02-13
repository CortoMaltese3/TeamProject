using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamProject.Models
{
    public class BranchWithDistance 
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public double Longtitude { get; set; }
        public double Latitude { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public virtual ICollection<Court> Court { get; set; }
        public virtual ICollection<Review> Review { get; set; }
        public virtual ICollection<Facility> Facility { get; set; }
        public double Distance { get; set; }
    }
}