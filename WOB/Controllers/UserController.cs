using Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace WOB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IServiceManager? _serviceManager;

        public UserController(IServiceManager serviceManager) => _serviceManager = serviceManager;

        [HttpGet]
        public async Task<IActionResult> GetAllUser(CancellationToken cancellationToken = default)
        {
            var users = await _serviceManager.UserService.GetAllAsync(cancellationToken);

            if(users == null)
            {
                return BadRequest("Unable to get data of users.");
            }

            return Ok(users);
        }

        [Authorize]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(string? userId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest($"Invalid Request. {nameof(userId)} cannot be null or empty.");
            }

            var user = await _serviceManager.UserService.GetByIdAsync(userId, cancellationToken);

            if(user == null)
            {
                return BadRequest("Unable to get user.");
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto? userDto, CancellationToken cancellationToken = default)
        {
            if(userDto == null)
            {
                return BadRequest("Unable to register a new user.");
            }

            var user = await _serviceManager.UserService.CreateAsync(userDto, cancellationToken);

            return Ok(user);
        }

        [Authorize]
        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(string? userId, [FromBody] UpdateUserDto? userDto, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest($"Invalid Request. {nameof(userId)} cannot be null or empty.");
            }

            if(userDto == null)
            {
                return BadRequest("Unable to update the user.");
            }

            await _serviceManager.UserService.UpdateAsync(userId, userDto, cancellationToken);

            return Ok();
        }
    }
}
