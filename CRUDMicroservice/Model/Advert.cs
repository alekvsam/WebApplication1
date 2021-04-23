using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CRUDMicroservice.Model
{    
    public class Advert
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int AdvertId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public float Price { get; set; }

        public Advert() { }
    }
}
