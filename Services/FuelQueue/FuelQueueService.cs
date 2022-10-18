using System;
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
            Console.Write(settings.ConnectionString);
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _fuelQueue = database.GetCollection<FuelQueue>(settings.FuelQueueCollectionName);
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
