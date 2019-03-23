using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamProject.ModelsViews
{
    public class BookingsSearchForm
    {
        public int Id { get; set; }
        public int? CourtId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}