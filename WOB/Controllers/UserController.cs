using Contracts;
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
                throw new NullReferenceException(nameof(users));
            }

            return Ok(users);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(string? userId, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(userId);
            }

            var user = await _serviceManager.UserService.GetByIdAsync(userId, cancellationToken);

            if(user == null)
            {
                throw new NullReferenceException(nameof(user));
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task RegisterUser(RegisterUserDto? userDto, CancellationToken cancellationToken = default)
        {
            if(userDto == null)
            {
                throw new ArgumentNullException(nameof(userDto));
            }

            await _serviceManager.UserService.CreateAsync(userDto, cancellationToken);
        }

        [HttpPut("{userId}")]
        public async Task UpdateUser(string? userId, UpdateUserDto? userDto, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if(userDto == null)
            {
                throw new ArgumentNullException(nameof(userDto));
            }

            await _serviceManager.UserService.UpdateAsync(userId, userDto, cancellationToken);
        }
    }
}
