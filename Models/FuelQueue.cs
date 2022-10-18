using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace fuel_queue_server.Models
{
    public class FuelQueue
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = String.Empty;

        [BsonElement("fuelStationId")]
        public string FuelStationId { get; set; } = String.Empty;

        [BsonElement("numberOfVehicle")]
        public int NumberOfVehicle { get; set; }

        [BsonElement("userId")]
        public int UserId { get; set; }
        public FuelQueue() { }
    }
}
