using Contracts.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace WOB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IServiceManager? _serviceManager;

        public UserController(IServiceManager serviceManager) => _serviceManager = serviceManager;

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllUser(CancellationToken cancellationToken = default)
        {
            var users = await _serviceManager.UserService.GetAllAsync(cancellationToken);

            if(users == null)
            {
                return BadRequest("Unable to get the data of users.");
            }

            return Ok(users);
        }

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

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto? userDto, CancellationToken cancellationToken = default)
        {
            var result = await _serviceManager.UserService.CreateAsync(userDto, cancellationToken);

            if (!result)
            {
                return BadRequest("Something went wrong. Unable to register a new user.");
            } 
            
            return Ok();
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(string? userId, [FromBody] UpdateUserDto? userDto, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest($"Invalid Request. {nameof(userId)} cannot be null or empty.");
            }

            var result = await _serviceManager.UserService.UpdateAsync(userId, userDto, cancellationToken);

            if (!result)
            {
                return BadRequest("Something went wrong. Unable to update the user.");
            }

            return Ok();
        }
    }
}
