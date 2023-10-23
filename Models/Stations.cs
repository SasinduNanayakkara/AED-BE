/**
 * @Author H.M.S.Y. Nanayakkara
 * @Created 10/5/2023
 * @Description Implement StationModel for Trains
 **/
using MongoDB.Bson.Serialization.Attributes;

namespace AED_BE.Models
{
    [BsonIgnoreExtraElements]
    public class Stations //Stations model
    {
        [BsonElement("station")]
        public string Station { get; set; }
        [BsonElement("time")]
        public string Time { get; set; }

    }
}
