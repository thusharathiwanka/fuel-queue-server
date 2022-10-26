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
        bool AddUsersToQueue(QueueCustomer queueCustomer, string fuelStation);
        void RemoveUsersFromQueue(string fuelStation, string customer, string detailedStatus);
        void Delete(string id);
    }
}
