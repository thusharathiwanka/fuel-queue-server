using Microsoft.AspNetCore.Mvc;
using fuel_queue_server.Models;
using fuel_queue_server.Services;

namespace fuel_queue_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuelQueueController : ControllerBase
    {

        private readonly IFuelQueueService fuelQueueService;

        public FuelQueueController(IFuelQueueService fuelQueueService)
        {
            this.fuelQueueService = fuelQueueService;
        }

        // GET: api/<UserController>
        [HttpGet]
        public ActionResult<List<FuelQueue>> Get()
        {
            return fuelQueueService.Get();
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public ActionResult<FuelQueue> Get(String id)
        {
            var fuelQueue = fuelQueueService.Get(id);
            if (fuelQueue == null)
            {
                return NotFound($"FuelQueue with Id = {id} not found");
            }

            return fuelQueue;
        }

        // POST api/<UserController>
        [HttpPost]
        public ActionResult<FuelQueue> Post([FromBody] FuelQueue fuelQueue)
        {
            fuelQueueService.Create(fuelQueue);
            return CreatedAtAction(nameof(Get), new { id = fuelQueue.Id }, fuelQueue);
        }

        // POST api/<UserController>
        [HttpPost("/join/{id}")]
        public ActionResult<FuelQueue> Post(string id, [FromBody] QueueCustomer queueCustomer)
        {
            bool isUpdated = fuelQueueService.AddUsersToQueue(queueCustomer, id);

            return CreatedAtAction(nameof(Get), new { status = isUpdated }, queueCustomer);
        }

        // PUT api/<UserController>
        [HttpPut("{id}")]
        public ActionResult Put(String id, [FromBody] FuelQueue fuelQueue)
        {
            var existingFuelQueue = fuelQueueService.Get(id);

            if (existingFuelQueue == null)
            {
                return NotFound($"FuelQueue with Id = {id} not found");
            }

            fuelQueueService.Update(id, fuelQueue);

            return NoContent();
        }

        // DELETE api/<StudentController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(String id)
        {
            var fuelQueue = fuelQueueService.Get(id);

            if (fuelQueue == null)
            {
                return NotFound($"FuelQueue with Id = {id} not found");
            }

            fuelQueueService.Delete(fuelQueue.Id);

            return Ok($"FuelQueue with Id = {id} deleted");
        }
    }
}
