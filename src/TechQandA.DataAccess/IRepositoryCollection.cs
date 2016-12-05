using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TechQandA.DataAccess
{
    /// <summary>
    /// Repository Collection Interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepositoryCollection<T>
    {
        /// <summary>
        /// Creates the collection if not exists asynchronously.
        /// </summary>
        /// <returns></returns>
        Task CreateCollectionIfNotExistsAsync();

        /// <summary>
        /// Creates the item entry inside collection asynchronously.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        Task<T> CreateAsync(T item);

        /// <summary>
        /// Updates the item inside collection asynchronously.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        Task<T> UpdateAsync(string id, T item);

        /// <summary>
        /// Deletes the item inside collection asynchronously.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<T> DeleteAsync(string id);

        /// <summary>
        /// Gets the item inside collection asynchronously.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<T> GetAsync(string id);

        /// <summary>
        /// Gets multiple items entry inside collection asynchronously.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate);
    }
}
