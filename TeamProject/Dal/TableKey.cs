using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeamProject.Dal
{
    public class TableKey : Attribute
    {
        public readonly string Keys;
        public TableKey()
        {

        }
        public TableKey(string keys)
        {
            Keys = keys;
        }
    }
}