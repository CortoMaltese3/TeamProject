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
        protected TableManager()
        {
            // use TableFieldAttribute on Models for auto field mapping
            MapTableFields();
        }

        public IEnumerable<T> All => Get();

        public virtual T Add(T row)
        {
            T addedCourt = default(T);

            _db.UsingConnection((dbCon) =>
            {
                addedCourt = dbCon.Query<T>(
                    $"INSERT INTO [{TableName}] ({InsertFields}) " +
                    $"VALUES ({InsertValues}) " +
                    $"SELECT * FROM [{TableName}] WHERE {ScopeKey}",
                    row).FirstOrDefault();
            });

            return addedCourt;
        }

        public virtual T Find(int id)
        {
            return Get($"[{TableName}].[{TableKey}] = @id",
                new { id }).FirstOrDefault();
        }

        public abstract IEnumerable<T> Get(string query = null, object parameters = null);

        public virtual bool Remove(int id)
        {
            int rowsAffected = 0;
            _db.UsingConnection((dbCon) =>
            {
                rowsAffected = dbCon.Execute(
                    $"DELETE " +
                    $"FROM [{TableName}] " +
                    $"WHERE [{TableKey}] = @id",
                    new { id });
            });
            return rowsAffected > 0;
        }

        public virtual bool Update(T row)
        {
            int rowsAffected = 0;
            _db.UsingConnection((dbCon) =>
            {
                rowsAffected = dbCon.Execute(
                    $"UPDATE [{TableName}] SET " +
                    $"{UpdateFields} " +
                    $"WHERE [{TableKey}] = @id", row);
            });
            return rowsAffected > 0;
        }

        #region Field Mapping

        protected string InsertFields;
        protected string InsertValues;
        protected string UpdateFields;
        protected string TableName;
        protected string TableKey;
        protected string ScopeKey;

        private void MapTableFields()
        {
            var tableType = typeof(T);

            // get table name
            TableName = tableType.Name;

            // get model fields using TableKeyAttribute
            var tableFields = tableType.GetProperties().Where(HavingTableKeyAttribute);

            // find primary key (or keys for middle tables)
            var tableKeys = tableFields.Where(IsPrimaryKey);

            // compose string with field names used to insert query (Fields)
            InsertFields = string.Join(", ", tableFields.Where(FieldsForInsert).Select(p => $"[{p.Name}]"));

            // compose string with field names used to insert query (@Values)
            InsertValues = string.Join(", ", tableFields.Where(FieldsForInsert).Select(p => $"@{p.Name}"));
            
            // compose string with field names used to update query (Fields=Values)
            UpdateFields = string.Join(", ", tableFields.Where(FieldsForUpdate).Select(p => $"[{p.Name}]=@{p.Name}"));

            // if more than one primary keys found (usually on middle tables) 
            // get first key to use as table key 
            // or use default key name (Id) 
            TableKey = tableKeys.FirstOrDefault()?.Name ?? "Id";

            // if more than one primary keys found (usually on middle tables) 
            // then use a combination of those keys to get inserted record 
            // or use default tablekey (Id) with SCOPE_IDENTITY
            ScopeKey = tableKeys.Count() > 1
                ? string.Join(" AND ", tableKeys.Select(k => $"[{k.Name}] = @{k.Name}"))
                : $"[{TableKey}] = (SELECT SCOPE_IDENTITY())";
        }

        private bool IsPrimaryKey(PropertyInfo propertyInfo)
        {
            return propertyInfo.GetCustomAttribute<TableFieldAttribute>().PrimaryKey;
        }

        private bool HavingTableKeyAttribute(PropertyInfo propertyInfo)
        {
            return propertyInfo.GetCustomAttribute<TableFieldAttribute>() != null;
        }

        private bool FieldsForUpdate(PropertyInfo propertyInfo)
        {
            var attribute = propertyInfo.GetCustomAttribute<TableFieldAttribute>();
            return !attribute.ExcludeFromUpdate;
        }

        private bool FieldsForInsert(PropertyInfo propertyInfo)
        {
            var attribute = propertyInfo.GetCustomAttribute<TableFieldAttribute>();
            return !attribute.ExcludeFromInsert;
        }
        #endregion
    }
}