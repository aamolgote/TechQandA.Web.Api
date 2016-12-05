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
        }

        public void Initialize()
        {
           
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


    }
}
