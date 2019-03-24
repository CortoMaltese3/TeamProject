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

        public int? this[int day]
        {
            get
            {
                int? dayId = 0;

                switch (day)
                {
                    case 1:
                        dayId = Day1;
                        break;
                    case 2:
                        dayId = Day2;
                        break;
                    case 3:
                        dayId = Day3;
                        break;
                    case 4:
                        dayId = Day4;
                        break;
                    case 5:
                        dayId = Day5;
                        break;
                    case 6:
                        dayId = Day6;
                        break;
                    case 7:
                        dayId = Day7;
                        break;

                }

                return dayId;
            }
        }
    }
}