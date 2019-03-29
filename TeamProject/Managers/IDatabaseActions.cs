using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamProject.Managers
{
    interface IDatabaseActions<T>
    {
        IEnumerable<T> Get(string query, object parameters);
        T Find(int id);
        T Add(T row);
        bool Remove(int id);
        bool Update(T row);
    }
}