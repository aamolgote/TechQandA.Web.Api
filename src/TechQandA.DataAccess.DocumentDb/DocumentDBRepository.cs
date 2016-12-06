using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechQandA.DataAccess.DocumentDb
{
    public class DocumentDBRepository<T> : IDatabaseRepository
    {
        private DocumentClient client;
        private string endPoint;
        private string authKey;

        public DocumentDBRepository()
        {
            endPoint = "https://techqanda.documents.azure.com:443/";
            authKey = "MctcfxAqTutrlSlnmh4vrgPqNSUq5Y7Es7B2XVmNq7Xvl1aRh4pbOtwEScantnw0jH21uJTclosuFZ8qfELTFg==";
            this.client = new DocumentClient(new Uri(endPoint), authKey);
            this.Initialize();
        }

        private void Initialize()
        {
            this.CreateDatabaseIfNotExistsAsync().Wait();
        }

        public string DatabaseId
        {
            get
            {
                return "techqanda";
            }
        }

        public DocumentClient Client
        {
            get
            {
                return client;
            }
        }

        public string EndPoint
        {
            get
            {
                return this.endPoint;
            }
        }

        public string AuthKey
        {
            get
            {
               return this.authKey;
            }
        }

        private async Task CreateDatabaseIfNotExistsAsync()
        {
            try
            {
                await this.client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(DatabaseId));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await this.client.CreateDatabaseAsync(new Database { Id = DatabaseId });
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
