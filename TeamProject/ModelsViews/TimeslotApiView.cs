using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeamProject.ModelsViews
{
    public class TimeslotApiView
    {
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}")]
        public TimeSpan Hour { get; set; }

        public int? Day1 { get; set; }
        public int? Day2 { get; set; }
        public int? Day3 { get; set; }
        public int? Day4 { get; set; }
        public int? Day5 { get; set; }
        public int? Day6 { get; set; }
        public int? Day7 { get; set; }
     
    }
}