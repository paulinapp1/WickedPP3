using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Wicked.Services
{
    public class WickedScores
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public DateTime Date {  get; set; }= DateTime.UtcNow;

    }
}
