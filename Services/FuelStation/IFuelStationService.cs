using fuel_queue_server.Models;

/*
* IFuleStationService: interface - Interface for manage fuel station operations on database
*/
namespace fuel_queue_server.Services
{
    public interface IFuelStationService
    {
        List<FuelStation> Get();
        FuelStation Get(string id);
        FuelStation Create(FuelStation fuelStation);
        void Update(string id, FuelStation fuelStation);
        void Delete(string id);
    }
}
