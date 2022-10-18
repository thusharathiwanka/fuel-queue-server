using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace fuel_queue_server.Models
{
    [BsonIgnoreExtraElements]
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = String.Empty;

        [BsonElement("username")]
        public string Username { get; set; } = String.Empty;

        [BsonElement("password")]
        public string Password { get; set; }

        [BsonElement("role")]
        public String Role { get; set; } = String.Empty;

        [BsonElement("vehicleType")]
        public String VehicleType { get; set; } = String.Empty;

        public User() { }
    }
}

