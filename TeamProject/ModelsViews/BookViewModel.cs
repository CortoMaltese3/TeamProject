using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TeamProject.Models;

namespace TeamProject.ModelsViews
{
    public class BookViewModel
    {
        public int CourtId { get; set; }
        public IEnumerable<Court> Courts { get; set; }
    }
}