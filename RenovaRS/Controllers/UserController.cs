using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RenovaRS.Data.Context;
using RenovaRS.Models;

namespace RenovaRS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return Ok(await userRepository.GetUsersAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await userRepository.GetUserAsync(id);

            if (user is null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            return Ok(user);
        }

        [HttpGet("{id}/services")]
        public async Task<ActionResult<Service>> GetServicesByUserId(int id)
        {
            var services = await this.userRepository.GetServicesByUserIdAsync(id);

            if (services.IsNullOrEmpty())
            {
                return NotFound($"Found no Services for User with ID {id}.");
            }

            return Ok(services);
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<User>>> PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await userRepository.RegisterUserAsync(user);

            return Ok(await userRepository.GetUsersAsync());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<User>>> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            await userRepository.UpdateUserAsync(user);

            return Ok(await userRepository.GetUsersAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<User>>> DeleteUser(int id)
        {
            var dbAddress = await userRepository.GetUserAsync(id);

            if (dbAddress is null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            await userRepository.DeleteUserAsync(id);

            return Ok(await userRepository.GetUsersAsync());
        }
    }
}
