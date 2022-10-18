using fuel_queue_server.Models;
using fuel_queue_server.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace fuel_queue_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuelStationController : ControllerBase

    {
        private readonly IFuelStationService fuelStationService;

        public FuelStationController(IFuelStationService fuelStationService)
        {
            this.fuelStationService = fuelStationService;
        }

        // GET: api/<FuelStationController>
        [HttpGet]
        public ActionResult<List<FuelStation>> Get()
        {
            return fuelStationService.Get();
        }

        // GET api/<FuelStationController>/5
        [HttpGet("{id}")]
        public ActionResult<FuelStation> Get(String id)
        {
            var fuelStation = fuelStationService.Get(id);
            if (fuelStation == null)
            {
                return NotFound($"FuelStation with Id = {id} not found");
            }

            return fuelStation;
        }

        // POST api/<FuelStationController>
        [HttpPost]
        public ActionResult<FuelStation> Post([FromBody] FuelStation fuelStation)
        {
            fuelStationService.Create(fuelStation);
            return CreatedAtAction(nameof(Get), new { id = fuelStation.Id }, fuelStation);
        }

        // PUT api/<FuelStationController>/5
        [HttpPut("{id}")]
        public ActionResult Put(String id, [FromBody] FuelStation fuelStation)
        {
            var existingStation= fuelStationService.Get(id);

            if (existingStation == null)
            {
                return NotFound($"FuelStationService with Id = {id} not found");
            }

            fuelStationService.Update(id, fuelStation);

            return NoContent();
        }

        // DELETE api/<FuelStationController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(String id)
        {
            var fuelStation = fuelStationService.Get(id);

            if (fuelStation == null)
            {
                return NotFound($"FuelStation with Id = {id} not found");
            }

            fuelStationService.Delete(fuelStation.Id);

            return Ok($"FuelStation with Id = {id} deleted");
        }
    }
}
