using CRUDMicroservice.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CRUDMicroservice.Data
{
    public class AdvertsContext
    {
        private readonly IMongoDatabase _database = null;

        public AdvertsContext(IOptions<CosmosDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<Advert> Adverts
        {
            get
            {
                return _database.GetCollection<Advert>("Adverts");
            }
        }
    }
}
