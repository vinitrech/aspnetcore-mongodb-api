using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace aspnet_core_mongo_api.Models
{
    public class Driver
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Name { get; set; } = null!;

        public int Number { get; set; }

        public string Team { get; set; } = null!;


    }
}
