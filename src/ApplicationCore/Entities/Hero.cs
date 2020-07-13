using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ApplicationCore.Entities
{
    public class Hero
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string HeroId { get; set; }

        [BsonElement("firstName")]
        public string FirstName { get; set; }

        [BsonElement("lastName")]
        public string LastName { get; set; }

        [BsonElement("house")]
        public string House { get; set; }

        [BsonElement("knownAs")]
        public string KnownAs { get; set; }

        [BsonElement("extraInfo")]
        [BsonIgnoreIfNull]
        public string ExtraInfo { get; set; }
    }
}