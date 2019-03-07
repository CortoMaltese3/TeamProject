using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
namespace TeamProject.Models
{
    public abstract class TableManager<T> : IDatabaseActions<T>
    {
        protected ProjectDbContext _db;
        protected Dictionary<string, string> _queryParts;
        public IEnumerable<T> Court
        {
            get
            {
                return Get();
            }
        }

        public virtual T Add(T row)
        {
            T addedCourt = default(T);
            _db.UsingConnection((dbCon) =>
            {
                addedCourt = dbCon
                    .Query<T>(_queryParts["InsertQuery"], row)
                    .FirstOrDefault();
            });

            return addedCourt;
        }

        public T Find(int id)
        {
            return Get(_queryParts["FindById"], new { id }).FirstOrDefault();
        }

        public abstract IEnumerable<T> Get(string query = null, object parameters = null);

        public virtual bool Remove(int id)
        {
            int rowsAffected = 0;
            _db.UsingConnection((dbCon) =>
            {
                rowsAffected = dbCon
                    .Execute(_queryParts["RemoveQuery"], new { id });
            });
            return rowsAffected > 0;
        }

        public virtual bool Update(T row)
        {
            int rowsAffected = 0;
            _db.UsingConnection((dbCon) =>
            {
                rowsAffected = dbCon
                    .Execute(_queryParts["UpdateQuery"], row);
            });
            return rowsAffected > 0;
        }
    }
}