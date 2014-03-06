using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gilt.util;

namespace gilt.dblinq
{
    /// <summary>
    /// An abc that abstracts the linq statments from the client 
    /// </summary>
    /// <typeparam name="T">A proxied type database class</typeparam>
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        /// <summary>
        /// The gilt database DataContext
        /// </summary>
        protected giltdbDataContext DataContext { get; set; }                
        /// <summary>
        /// A querable list item that is used in all of the fetch methods
        /// </summary>
        protected IQueryable<T> _genericQuery = null;
        /// <summary>
        /// Create a connection to the gilt database data context
        /// </summary>
        public GenericRepository()        
        {
            DataContext = new giltdbDataContext(AppSettings.DatabaseConnectionString);
            this.InitializeQuery();
        }
        /// <summary>
        /// Create a connection to the gilt database data context
        /// </summary>
        /// <param name="dataContext">A gilt database datacontext instance</param>
        public GenericRepository(giltdbDataContext dataContext) 
        {            
            DataContext = dataContext;
            this.InitializeQuery();
        }
        /// <summary>
        /// Initialize the datacontext query
        /// </summary>
        protected abstract void InitializeQuery();

        #region IGenericRepository virtuals
        /// <summary>
        /// Find an item in the repository using a predicate function
        /// </summary>
        /// <param name="predicate">The predicate expression</param>
        /// <returns>An enumerable list of T</returns>
        public virtual IEnumerable<T> FindBy(Func<T, bool> predicate)
        {
            return _genericQuery.Where(predicate);
        }
        /// <summary>
        /// Get all items in the repository
        /// </summary>
        /// <returns>All items in repository</returns>
        public virtual IQueryable<T> GetAll()
        {
            return _genericQuery.Where(a => true);
        }
        /// <summary>
        /// Get the number of items in the repository
        /// </summary>
        /// <returns>The number of items</returns>
        public virtual int GetCount()
        {
            return this.GetAll().Count();
        }
        #endregion

        #region CRUD virtuals -- defaults don't do anything
        /// <summary>
        /// Update an existing item in the repository
        /// </summary>
        /// <param name="Proxy">The item to update</param>
        public virtual void Update(T Proxy)
        {            
        }
        /// <summary>
        /// Add an item to the repository
        /// </summary>
        /// <param name="Proxy">The item to add</param>
        public virtual void Add(T Proxy)
        {
        }
        /// <summary>
        /// Delete an item from the repository
        /// </summary>
        /// <param name="Proxy">The item to delete</param>
        public virtual void Delete(T Proxy)
        {
        }
        #endregion
    }
}
