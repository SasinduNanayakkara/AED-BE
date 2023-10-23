/**
 * @Author M.R.A. Perera
 * @Created 10/2/2023
 * @Description Implement Reservation Model
 **/
using MongoDB.Bson.Serialization.Attributes;

namespace AED_BE.Models
{
    [BsonIgnoreExtraElements]
    public class Reservation //Reservation model
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("reservationId")]
        public int ReservationId { get; set; }

        [BsonElement("client")]
        public Client Client { get; set; }

        [BsonElement("train")]
        public Trains Train { get; set; }

        [BsonElement("date")]
        public string Date { get; set; }


    }
}
