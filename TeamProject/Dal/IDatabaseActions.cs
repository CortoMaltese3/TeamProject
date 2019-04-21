using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamProject.Dal
{
    /// <summary>
    /// Interface for basic database actions (Get, Find, Add, Remove)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDatabaseActions<T>
    {
        IEnumerable<T> All { get; }

        /// <summary>
        /// Get all entity objects of T type 
        /// </summary>
        /// <param name="query">extra parameters in query</param>
        /// <param name="parameters">parameters for query</param>
        /// <returns></returns>
        IEnumerable<T> Get(string query=null, object parameters=null);

        /// <summary>
        /// Finds one entity of type T
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Find(int id);

        /// <summary>
        /// Inserts an entity of type T
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        T Add(T row);

        /// <summary>
        /// Removes an entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Remove(int id);

        /// <summary>
        /// Updates and entity
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        bool Update(T row);
    }
}