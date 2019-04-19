using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamProject.Dal
{
    public class TableExcludeField : Attribute
    {
        public bool FromUpdate { get; set; }
        public bool FromInsert { get; set; }
    }
}