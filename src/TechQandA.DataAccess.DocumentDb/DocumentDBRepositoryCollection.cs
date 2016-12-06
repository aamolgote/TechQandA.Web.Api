using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.Azure.Documents.Linq;

namespace TechQandA.DataAccess.DocumentDb
{
    /// <summary>
    /// Document DB Repository Implementation for generic repository.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="TechQandA.DataAccess.IRepositoryCollection{T}" />
    public class DocumentDBRepositoryCollection<T> : IRepositoryCollection<T>
    {
        #region private members
        private IDatabaseRepository db;
        private string collectionName;
        private DocumentClient client;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentDBRepositoryCollection{T}"/> class.
        /// </summary>
        /// <param name="collectionName">Name of the collection.</param>
        public DocumentDBRepositoryCollection(string collectionName) 
        {
            this.db = new DocumentDBRepository<T>();
            this.collectionName = collectionName;
            this.client = new DocumentClient(new Uri(this.db.EndPoint), this.db.AuthKey);
        }
        #endregion

        #region Interface Implementation Methods
        /// <summary>
        /// Creates the collection if not exists asynchronously.
        /// </summary>
        /// <returns></returns>
        public async Task CreateCollectionIfNotExistsAsync()
        {
            try
            {
                await this.client.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(db.DatabaseId, this.collectionName));
            }
            catch (DocumentClientException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await this.client.CreateDocumentCollectionAsync(
                        UriFactory.CreateDatabaseUri(db.DatabaseId),
                        new DocumentCollection { Id = this.collectionName },
                        new RequestOptions { OfferThroughput = 1000 });
                }
            }
        }

        /// <summary>
        /// Creates the item entry inside collection asynchronously.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public async Task<T> CreateAsync(T item)
        {
            var document = await this.client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(this.db.DatabaseId, this.collectionName), item);
            return (T)(dynamic)document.Resource; 
        }

        /// <summary>
        /// Updates the item inside collection asynchronously.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public async Task<T> UpdateAsync(string id, T item)
        {
            var document = await this.client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(this.db.DatabaseId, this.collectionName, id), item);
            return (T)(dynamic)document;
        }

        /// <summary>
        /// Deletes the item inside collection asynchronously.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<T> DeleteAsync(string id)
        {
            var document = await this.client.DeleteDocumentAsync(UriFactory.CreateDocumentUri(this.db.DatabaseId, this.collectionName, id));
            return (T)(dynamic)document;
        }

        /// <summary>
        /// Gets the item inside collection asynchronously.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<T> GetAsync(string id)
        {
            var document = await this.client.ReadDocumentAsync(UriFactory.CreateDocumentUri(this.db.DatabaseId, this.collectionName, id));
            return (T)(dynamic)document;
        }

        /// <summary>
        /// Gets multiple items entry inside collection asynchronously.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {

            IDocumentQuery<T> query = this.client.CreateDocumentQuery<T>(
                UriFactory.CreateDocumentCollectionUri(this.db.DatabaseId, this.collectionName),
                new FeedOptions { MaxItemCount = -1 })
                .Where(predicate)
                .AsDocumentQuery();

            List<T> results = new List<T>();
            while (query.HasMoreResults)
            {
                results.AddRange(await query.ExecuteNextAsync<T>());
            }

            return results;
        }
        #endregion
    }
}
