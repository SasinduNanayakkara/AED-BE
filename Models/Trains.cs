using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.Serialization.Attributes;

namespace AED_BE.Models
{
    [BsonIgnoreExtraElements]

    public class Trains
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("trainNo")]
        public int TrainNo { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("date")]
        public List<string> Date { get; set; }

        [BsonElement("stations")]
        public List<Stations> Stations { get; set; }

        public static implicit operator Trains(List<Trains> v)
        {
            throw new NotImplementedException();
        }

        public static implicit operator Trains(ActionResult<Trains> v)
        {
            throw new NotImplementedException();
        }
    }
}
