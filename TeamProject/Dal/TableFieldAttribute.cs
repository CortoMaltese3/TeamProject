using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeamProject.Dal
{
    public class TableFieldAttribute : Attribute
    {
        public bool ExcludeFromUpdate { get; set; }
        public bool ExcludeFromInsert { get; set; }
        public bool PrimaryKey { get; set; }
    }
}