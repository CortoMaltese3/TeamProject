using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using TeamProject.Managers;

namespace TeamProject.Dal
{
    /// <summary>
    /// Abstract class to be used for each entity of the project
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class TableManager<T> : IDatabaseActions<T>
    {
        protected ProjectDbContext _db;
        protected Dictionary<string, string> _queryParts;
        protected string TableName { get; }
        public TableManager()
        {
            TableName = typeof( T).Name;
        }
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
            return Get($"[{TableName}].id = @id", new { id }).FirstOrDefault();
        }

        public abstract IEnumerable<T> Get(string query = null, object parameters = null);

        public virtual bool Remove(int id)
        {
            int rowsAffected = 0;
            _db.UsingConnection((dbCon) =>
            {
                rowsAffected = dbCon
                    .Execute($"DELETE FROM [{TableName}] WHERE Id = @Id", new { id });
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