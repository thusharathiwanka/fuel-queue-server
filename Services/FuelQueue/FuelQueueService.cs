using System;
using fuel_queue_server.Models.Database;
using fuel_queue_server.Services;
using fuel_queue_server.Models;
using MongoDB.Driver;

/*
* Manages fuel queue operations
*/

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

        /*
         * Function - Adding users to the fuel queue
         * Params - queueCustomer(QueueCustomer) - QueueCustomer object to add customers array
         *        - fuelStation(string) - id of fuel station
         * Returns - boolean (fuel queue updated status)
         */
        public bool AddUsersToQueue(QueueCustomer queueCustomer, string fuelStation)
        {
            // Auto generating entering time
            queueCustomer.enteredTime = DateTime.Now.ToString("MM/dd/yyyy HH:mm");
            queueCustomer.exitedTime = "";

            // Filtering fuelQueue using fuel station id
            var fuelStationFilter = Builders<FuelQueue>
             .Filter.Eq(e => e.FuelStationId, fuelStation);

            // Pushing queue customer to the customers array
            var fuelQueueUpdate = Builders<FuelQueue>.Update
                    .Push(e => e.Customers, queueCustomer);

            // Incrementing total number of vehicles in the queue
            var fuelQueueIncrementUpdate = Builders<FuelQueue>.Update
                    .Inc(e => e.NumberOfVehicles, 1);

            // Commiting updates to database
            var fuelQueueUpdateResult = _fuelQueue.UpdateOne(fuelStationFilter, fuelQueueUpdate);
            var incrementUpdateResult = _fuelQueue.UpdateOne(fuelStationFilter, fuelQueueIncrementUpdate);

            // Return update status
            if (fuelQueueUpdateResult == null || incrementUpdateResult == null) {
                return false;
            }

            return true;

        }

        /*
         * Function - Removing users from the fuel queue
         * Params - fuelStation(string) - id of fuel station
         *        - customer(string) - id of vehicle owner
         *        - detailsedStatus(string) - reason to leave fuel queue (exit before pump / exit after pump)
         * Returns - boolean (fuel queue updated status)
         */
        public bool RemoveUsersFromQueue(string fuelStation, string customer, string detailedStatus)
        {
            // Finding fuel queue using fuel station id
            var fuelQueue = _fuelQueue.Find(fuelQueue => fuelQueue.FuelStationId == fuelStation).FirstOrDefault();

            // Making customers list in the fuel queue
            List<QueueCustomer> queueCustomers = fuelQueue.Customers.ToList();

            // Updating status and exited time using vehicle owner id
            foreach (QueueCustomer queueCustomer in queueCustomers)
            {
                if (queueCustomer.UserId == customer && queueCustomer.Status)
                {
                    queueCustomer.Status = false;
                    queueCustomer.DetailedStatus = detailedStatus;
                    queueCustomer.exitedTime = DateTime.Now.ToString("MM/dd/yyyy HH:mm");
                }
            }

            // Filtering fuelQueue using fuel station id
            var fuelStationFilter = Builders<FuelQueue>
             .Filter.Eq(e => e.FuelStationId, fuelStation);

            // Setting queue customers array
            var fuelQueueUpdate = Builders<FuelQueue>.Update.Set("customers", queueCustomers);

            // Decrementing total number of vehicles in the queue
            var fuelQueueDecrementUpdate = Builders<FuelQueue>.Update
                   .Inc(e => e.NumberOfVehicles, -1);

            // Commiting updates to database
            var fuelQueueUpdateResult = _fuelQueue.UpdateOne(Builders<FuelQueue>
             .Filter.Eq(e => e.FuelStationId, fuelStation), fuelQueueUpdate);
            var decrementUpdateResult = _fuelQueue.UpdateOne(fuelStationFilter, fuelQueueDecrementUpdate);

            // Return update status
            if (fuelQueueUpdateResult == null || decrementUpdateResult == null)
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
