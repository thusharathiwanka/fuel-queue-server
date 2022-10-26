using fuel_queue_server.Models;
using fuel_queue_server.Models.Database;
using MongoDB.Driver;
using static System.Collections.Specialized.BitVector32;

namespace fuel_queue_server.Services
{
    public class FuelStationService : IFuelStationService
    {
        private readonly IMongoCollection<FuelStation> _fuelStation;


        public FuelStationService(IStoreDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _fuelStation = database.GetCollection<FuelStation>(settings.StationCollectionName);
        }

        FuelStation IFuelStationService.Create(FuelStation fuelStation)
        {
            _fuelStation.InsertOne(fuelStation);
            return fuelStation;
        }

        void IFuelStationService.Delete(string id)
        {
            _fuelStation.DeleteOne(fuelStation => fuelStation.Id == id);
        }

        List<FuelStation> IFuelStationService.Get()
        {
            return _fuelStation.Find(fuelStation => true).ToList();
        }

        FuelStation IFuelStationService.Get(string id)
        {
            return _fuelStation.Find(fuelStation => fuelStation.Id == id).FirstOrDefault();
        }

        void IFuelStationService.Update(string id, FuelStation fuelStation)
        {
            _fuelStation.ReplaceOne(fuelStation => fuelStation.Id == id, fuelStation);

        }
    }
}
