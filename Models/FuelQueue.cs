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

        [BsonElement("numberOfVehicles")]
        public int NumberOfVehicles { get; set; } = 0;

        [BsonElement("customers")]
        public QueueCustomer[] Customers { get; set; } = Array.Empty<QueueCustomer>();

        public FuelQueue() {}
    }
}
