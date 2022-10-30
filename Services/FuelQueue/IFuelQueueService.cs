﻿using fuel_queue_server.Models;
using System;

/*
* IFuelQueueService: interface - Interface for manage fuel queue operations on database
*/
namespace fuel_queue_server.Services
{
    public interface IFuelQueueService
    {
        List<FuelQueue> Get();
        FuelQueue Get(string id);
        FuelQueue Create(FuelQueue fuelQueue);
        FuelQueue GetFuelQueueByFuelStationId(string id);
        void Update(string id, FuelQueue fuelQueue);
        bool AddUsersToQueue(QueueCustomer queueCustomer, string fuelStation);
        bool RemoveUsersFromQueue(string fuelStation, string customer, string detailedStatus);
        void Delete(string id);
    }
}
