using MongoDB.Bson.Serialization.Attributes;

namespace AED_BE.Models
{
    [BsonIgnoreExtraElements]
    public class Stations
    {
        [BsonElement("station")]
        public string station { get; set; }
        [BsonElement("time")]
        public string time { get; set; }
        
    }
}
