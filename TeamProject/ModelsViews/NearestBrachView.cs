using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TeamProject.Models;

namespace TeamProject.ModelsViews
{
    public class NearestBrachView
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public IEnumerable<Branch> Branches { get; set; }
    }
}