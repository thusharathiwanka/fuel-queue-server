using System;
namespace fuel_queue_server.Controllers.Database
{
    public interface IStoreDatabaseSettings
    {
        string UserCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}

