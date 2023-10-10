using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AED_BE.Models
{
    [BsonIgnoreExtraElements]
    public class Client
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("firstname")]
        public string FirstName { get; set; } = "Client first name";

        [BsonElement("lastname")]
        public string LastName { get; set; } = "Client last name";

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("phone")]
        public string Phone { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }

        [BsonElement("isActive")]
        public Boolean IsActive { get; set; }

        [BsonElement("nic")]
        public string NIC { get; set; }
    }
}
