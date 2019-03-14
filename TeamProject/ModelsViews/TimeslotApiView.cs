using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamProject.ModelsViews
{
    public class TimeslotApiView
    {
        public TimeSpan Hour { get; set; }

        public int Day1 { get; set; }
        public int Day2 { get; set; }
        public int Day3 { get; set; }
        public int Day4 { get; set; }
        public int Day5 { get; set; }
        public int Day6 { get; set; }
        public int Day7 { get; set; }
     
    }
}