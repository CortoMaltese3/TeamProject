using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamProject.ModelsViews
{
    public class PostBookResponse
    {
        public string Status { get; set; }
        public int BookingId { get; set; }
        public string BookKey { get; set; }
    }
}