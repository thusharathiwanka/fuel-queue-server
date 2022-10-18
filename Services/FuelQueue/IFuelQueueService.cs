using fuel_queue_server.Models;
using System;
namespace fuel_queue_server.Services
{
    public interface IFuelQueueService
    {
        List<FuelQueue> Get();
        FuelQueue Get(string id);
        FuelQueue Create(FuelQueue fuelQueue);
        void Update(string id, FuelQueue fuelQueue);
        void Delete(string id);
    }
}
