﻿using System;
using fuel_queue_server.Models.Database;
using fuel_queue_server.Services;
using fuel_queue_server.Models;
using MongoDB.Driver;

namespace fuel_queue_server.Services
{
    public class FuelQueueService : IFuelQueueService
    {
        private readonly IMongoCollection<FuelQueue> _fuelQueue;

        public FuelQueueService(IStoreDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _fuelQueue = database.GetCollection<FuelQueue>(settings.FuelQueueCollectionName);
        }

        public bool AddUsersToQueue(QueueCustomer queueCustomer, string fuelStation)
        {
            var filter = Builders<FuelQueue>
             .Filter.Eq(e => e.FuelStationId, fuelStation);

            var update = Builders<FuelQueue>.Update
                    .Push(e => e.Customers, queueCustomer);

            var result = _fuelQueue.UpdateOne(filter, update);

            if (result == null) {
                return false;
            }

            return true;

        }

        public bool RemoveUsersFromQueue(string fuelStation, string customer, string detailedStatus)
        {
            var fuelQueue = _fuelQueue.Find(fuelQueue => fuelQueue.FuelStationId == fuelStation).FirstOrDefault();

            List<QueueCustomer> queueCustomers = fuelQueue.Customers.ToList();

            foreach(QueueCustomer queueCustomer in queueCustomers)
            {
                if(queueCustomer.UserId == customer)
                {
                    queueCustomer.Status = false;
                    queueCustomer.DetailedStatus = detailedStatus;
                }
            }

            var filter = Builders<FuelQueue>
             .Filter.Eq(e => e.FuelStationId, fuelStation);
            var update = Builders<FuelQueue>.Update.Set("customers", queueCustomers);

            var result = _fuelQueue.UpdateOne(Builders<FuelQueue>
             .Filter.Eq(e => e.FuelStationId, fuelStation), update);

            if (result == null)
            {
                return false;
            }

            return true;
        }

        public FuelQueue Create(FuelQueue fuelQueue)
        {
            _fuelQueue.InsertOne(fuelQueue);
            return fuelQueue;
        }

        public void Delete(string id)
        {
            _fuelQueue.DeleteOne(fuelQueue => fuelQueue.Id == id);
        }

        public List<FuelQueue> Get()
        {
            return _fuelQueue.Find(fuelQueue => true).ToList();
        }

        public FuelQueue Get(string id)
        {
            return _fuelQueue.Find(fuelQueue => fuelQueue.Id == id).FirstOrDefault();
        }

        public void Update(string id, FuelQueue fuelQueue)
        {
            _fuelQueue.ReplaceOne(fuelQueue => fuelQueue.Id == id, fuelQueue);
        }
    }
}
