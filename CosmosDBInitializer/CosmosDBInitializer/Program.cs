using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace CosmosDBInitializer
{
    class Program
    {
        private const string EndpointUri = "URI";
        private const string PrimaryKey = "PRIMARYKEY";

        private const string dbName = "dojodb";
        private const string collectionName = "locomotives";

        private DocumentClient client;

        static void Main(string[] args)
        {
            Console.WriteLine("Initialize Cosmos DB");

            var locomotives = new List<Locomotive>();
            locomotives.Add(new Locomotive { Baureihe = "103", Typ = "E", Laenge = 19500, Dienstmasse = 114, Vmax = 200 });
            locomotives.Add(new Locomotive { Baureihe = "01", Typ = "Dampf", Laenge = 23940, Dienstmasse = 109, Vmax = 120 });
            locomotives.Add(new Locomotive { Baureihe = "52", Typ = "Dampf", Laenge = 22975, Dienstmasse = 84, Vmax = 80 });
            locomotives.Add(new Locomotive { Baureihe = "V200", Typ = "Diesel", Laenge = 18470, Dienstmasse = 80, Vmax = 140 });
            locomotives.Add(new Locomotive { Baureihe = "151", Typ = "E", Laenge = 19490, Dienstmasse = 118, Vmax = 120 });

            try
            {
                Program p = new Program();
                p.AccessCosmosDb().Wait();

                p.client.CreateDatabaseIfNotExistsAsync(new Database { Id = dbName }).Wait();

                p.client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(dbName), new DocumentCollection { Id = collectionName }).Wait();

                foreach (var locomotive in locomotives)
                {
                    p.InsertDocument(dbName, collectionName, locomotive).Wait();
                }
            }
            catch (DocumentClientException de)
            {
                Exception baseException = de.GetBaseException();
                Console.WriteLine($"{de.StatusCode} error occured: {de.Message}, Message: {baseException.Message}");
            }
            catch (Exception e)
            {
                Exception baseException = e.GetBaseException();
                Console.WriteLine($"Error: {e.Message}, Message: {baseException.Message}");
            }
            finally
            {
                Console.WriteLine("End of CosmosDB access, press any key to exit.");
                Console.ReadKey();
            }
        }

        private async Task AccessCosmosDb()
        {
            this.client = new DocumentClient(new Uri(EndpointUri), PrimaryKey);
        }

        private async Task InsertDocument(string databaseName, string collectionName, Locomotive locomotive)
        {
            try
            {
                await this.client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(databaseName, collectionName), locomotive);
                Console.WriteLine($"Insert Baureihe {locomotive.Baureihe}");
            }
            catch (DocumentClientException de)
            {
                Exception baseException = de.GetBaseException();
                Console.WriteLine($"Error: {de.Message}, Message: {baseException.Message}");
            }

        }
    }
}
