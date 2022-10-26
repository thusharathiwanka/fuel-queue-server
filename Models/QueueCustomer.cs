using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace fuel_queue_server.Models
{
    [BsonIgnoreExtraElements]
    public class QueueCustomer
    {
        [BsonElement("userId")]
        public String UserId { get; set; } = String.Empty;

        [BsonElement("status")]
        public bool Status { get; set; } = true;

        [BsonElement("detailedStatus")]
        public string DetailedStatus { get; set; } = String.Empty;

        [BsonElement("enteredTime")]
        public string enteredTime { get; set; } = String.Empty;

        [BsonElement("exitedTime")]
        public string exsitedTime { get; set; } = String.Empty;

        public QueueCustomer() {}
    }
}
