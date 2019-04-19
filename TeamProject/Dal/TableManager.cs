using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using Dapper;
using TeamProject.Managers;
using System.Diagnostics;

namespace TeamProject.Dal
{
    /// <summary>
    /// Abstract class to be used for each entity of the project
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class TableManager<T> : IDatabaseActions<T>
    {
        protected ProjectDbContext _db;
        private List<PropertyInfo> _propertyInfos = new List<PropertyInfo>();
        private string _insertQuery;
        private string _updateQuery;
        private string _deleteQuery;
        private string _findQuery;

        public IEnumerable<T> Court => Get();

        public virtual T Add(T row)
        {
            T addedCourt = default(T);

            _db.UsingConnection((dbCon) =>
            {
                addedCourt = dbCon.Query<T>(_insertQuery, row)
                    .FirstOrDefault();
            });

            return addedCourt;
        }

        public T Find(int id)
        {
            return Get(_findQuery, new { id }).FirstOrDefault();
        }

        public abstract IEnumerable<T> Get(string query = null, object parameters = null);

        public virtual bool Remove(int id)
        {
            int rowsAffected = 0;
            _db.UsingConnection((dbCon) =>
            {
                rowsAffected = dbCon.Execute(_deleteQuery, new { id });
            });
            return rowsAffected > 0;
        }

        public virtual bool Update(T row)
        {
            int rowsAffected = 0;
            _db.UsingConnection((dbCon) =>
            {
                rowsAffected = dbCon.Execute(_updateQuery, row);
            });
            return rowsAffected > 0;
        }

        #region Field Mapping

        protected void AddField<U>(Expression<Func<T, U>> expression)
        {
            var memberExpression = expression.Body as MemberExpression;

            if (memberExpression == null)
            {
                throw new ArgumentNullException();
            }

            var propertyInfo = memberExpression.Member as PropertyInfo;
            _propertyInfos.Add(propertyInfo);

        }

        private string GetKey()
        {
            return typeof(T)
              .GetProperties()
              .Where(p => p.Name.Equals("id", StringComparison.InvariantCultureIgnoreCase) || 
                          p.GetCustomAttribute<TableKey>() != null)
              .FirstOrDefault()
              .GetCustomAttribute<TableKey>()
              ?.Keys ?? "Id";
        }

        protected void PrepareQueries()
        {
            var tableName = typeof(T).Name;
            var tableKeys = GetKey().Split(',');
            var tableKey = tableKeys[0];
            var scopeKey = $"[{tableKey}] = (SELECT SCOPE_IDENTITY())";

            if (tableKeys.Count() > 1)
            {
                scopeKey = string.Join(" AND ", tableKeys.Select(k => $"[{k}] = @{k}"));
            }
            
            _insertQuery =
                $"INSERT INTO [{tableName}] ({GetFieldsForInsert}) " +
                $"VALUES ({GetFieldsForValues}) " +
                $"SELECT * FROM [{tableName}] WHERE {scopeKey}";

            _updateQuery =
                $"UPDATE [{tableName}] SET " +
                $"{GetFieldsForUpdate} " +
                $"WHERE [{tableKey}] = @id";

            _deleteQuery =
                $"DELETE " +
                $"FROM [{tableName}] " +
                $"WHERE [{tableKey}] = @id";

            _findQuery = $"[{tableName}].[{tableKey}] = @id";

        }

        private string GetFieldsForInsert => string.Join(", ", _propertyInfos.Select(p => $"[{p.Name}]"));
        private string GetFieldsForValues => string.Join(", ", _propertyInfos.Select(p => $"@{p.Name}"));
        private string GetFieldsForUpdate => string.Join(", ", _propertyInfos.Where(p => (p.GetCustomAttribute<TableExcludeField>()?.FromUpdate ?? false) == false).Select(p => $"[{p.Name}]=@{p.Name}"));
        #endregion
    }
}