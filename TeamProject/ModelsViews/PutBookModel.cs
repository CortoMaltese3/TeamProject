using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamProject.ModelsViews
{
    public class PutBookModel
    {
        public int CourtId { get; set; }
        public DateTime BookedAt { get; set; }
    }
}