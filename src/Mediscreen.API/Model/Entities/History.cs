using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MediscreenAPI.Model.Entities
{
    public class Note
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public int? PatId { get; set; }
        public string? Note { get; set; }
    }
}
