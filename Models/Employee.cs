/**
 * @Author S.P. Rupasinghe
 * @Created 10/4/2023
 * @Description Implement Employee Model
 **/
using MongoDB.Bson.Serialization.Attributes;

namespace AED_BE.Models
{
    [BsonIgnoreExtraElements]
    public class Employee
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("email")]
        public string Email { get; set; } = null;

        [BsonElement("password")]
        public string Password { get; set; } = null;

        [BsonElement("role")]
        public string Role { get; set; } = null;
    }
}
