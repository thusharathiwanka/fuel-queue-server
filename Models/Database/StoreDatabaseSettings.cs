using System;
namespace fuel_queue_server.Models.Database
{
    public class StoreDatabaseSettings : IStoreDatabaseSettings
    {
        public StoreDatabaseSettings() {}

        public string UserCollectionName { get; set; } = String.Empty;
        public string FuelQueueCollectionName { get; set; } = String.Empty;
        public string ConnectionString { get; set; } = String.Empty;
        public string DatabaseName { get; set; } = String.Empty;
    }
}

