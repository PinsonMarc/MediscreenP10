using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MediscreenAPI.Model.Entities
{
    public class History
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public int? PatId { get; set; }
        public string Note { get; set; } = string.Empty;
    }
}
