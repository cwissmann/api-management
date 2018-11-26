using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

namespace APIapp
{
    public static class CosmosDBRepository<T> where T : class
    {
        private static readonly string Endpoint = "URI";
        private static readonly string PrimaryKey = "KEY";

        private static readonly string DatabaseId = "DATABASEID";
        private static readonly string CollectionId = "COLLECTIONID";

        private static DocumentClient client;

        public static void Initialize()
        {
            client = new DocumentClient(new Uri(Endpoint), PrimaryKey);
        }

        public static async Task<IEnumerable<T>> GetItemsAsync()
        {
            IDocumentQuery<T> query = client.CreateDocumentQuery<T>(
                    UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId))
                .AsDocumentQuery();

            List<T> results = new List<T>();

            while (query.HasMoreResults)
            {
                results.AddRange((await query.ExecuteNextAsync<T>()));
            }

            return results;
        }

        public static async Task<IEnumerable<T>> GetItemsAsync(Expression<Func<T, bool>> predicate)
        {
            IDocumentQuery<T> query = client.CreateDocumentQuery<T>(
                    UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId))
                .Where(predicate)
                .AsDocumentQuery();

            List<T> results = new List<T>();
            while (query.HasMoreResults)
            {
                results.AddRange(await query.ExecuteNextAsync<T>());
            }

            return results;
        }
    }
}
