using Microsoft.AspNetCore.Mvc;
using fuel_queue_server.Models;
using fuel_queue_server.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MongoDBTestProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        // GET: api/<UserController>
        [HttpGet]
        public ActionResult<List<User>> Get()
        {
            return userService.Get();
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public ActionResult<User> Get(String id)
        {
            var user = userService.Get(id);
            if (user == null)
            {
                return NotFound($"User with Id = {id} not found");
            }

            return user;
        }

        // POST api/<UserController>
        [HttpPost]
        public ActionResult<User> Post([FromBody] User user)
        {
            userService.Create(user);
            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        }

        // POST api/<UserController>
        [HttpPost("login")]
        public ActionResult<User> Login([FromBody] User request)
        {
            if (request.Username == null || request.Password == null || request.Role == null)
            {
                return BadRequest("Please provide username and password");
            }

            var existingUser = userService.Login(request.Username, request.Password, request.Role);

            if (existingUser == null)
            {
                return NotFound($"User with username = {request.Username} not found");
            }
           

            return Ok(existingUser);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public ActionResult Put(String id, [FromBody] User user)
        {

            var existingUser = userService.Get(id);
            
            if (existingUser == null)
            {
                return NotFound($"User with Id = {id} not found");
            }

            userService.Update(id, user);

            return NoContent();
        }

        // DELETE api/<StudentController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(String id)
        {
            var user = userService.Get(id);

            if (user == null)
            {
                return NotFound($"User with Id = {id} not found");
            }

            userService.Delete(user.Id);

            return Ok($"User with Id = {id} deleted");
        }
    }
}