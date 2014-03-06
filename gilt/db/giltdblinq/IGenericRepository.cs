using System;
using System.Collections.Generic;
using System.Linq;

namespace gilt.dblinq
{
    /// <summary>
    /// Define what a generic repository class exposes to a client
    /// </summary>
    /// <typeparam name="T">The proxied type</typeparam>
    public interface IGenericRepository<T> where T : class
    {
        /// <summary>
        /// Find an item in the repository using a predicate function
        /// </summary>
        /// <param name="predicate">The predicate expression</param>
        /// <returns>An enumerable list of T</returns>
        IEnumerable<T> FindBy(Func<T, bool> predicate);
        /// <summary>
        /// Get all items in the repository
        /// </summary>
        /// <returns>All items in repository</returns>
        IQueryable<T> GetAll();
        /// <summary>
        /// Get the number of items in the repository
        /// </summary>
        /// <returns>The number of items</returns>
        int GetCount();

        /// <summary>
        /// Update an existing item in the repository
        /// </summary>
        /// <param name="Proxy">The item to update</param>
        void Update(T Proxy);
        /// <summary>
        /// Add an item to the repository
        /// </summary>
        /// <param name="Proxy">The item to add</param>
        void Add(T Proxy);
        /// <summary>
        /// Delete an item from the repository
        /// </summary>
        /// <param name="Proxy">The item to delete</param>
        void Delete(T Proxy);
    }
}
