/**
 * @Author M.R.A. Perera
 * @Created 10/2/2023
 * @Description Implement Reservation Model
 **/
using MongoDB.Bson.Serialization.Attributes;

namespace AED_BE.Models
{
    [BsonIgnoreExtraElements]
    public class Reservation
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("reservationId")]
        public int ReservationId { get; set; }

        [BsonElement("nic")]
        public string NIC { get; set; }

        [BsonElement("trainId")]
        public int TrainNumber { get; set; }

        [BsonElement("date")]
        public string Date { get; set; }
    }
}
