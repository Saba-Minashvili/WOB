using AutoMapper;
using Contracts.ViewModels;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WOB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, Manager")]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<IdentityRole>? _roleManager;
        private readonly UserManager<User>? _userManager;
        private readonly IConfiguration? Configuration;
        private readonly IMapper? _mapper;

        public RoleController(
            RoleManager<IdentityRole>? roleManager, 
            UserManager<User> userManager,
            IMapper mapper,
            IConfiguration? configuration)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _mapper = mapper;
            Configuration = configuration;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllRoles(CancellationToken cancellationToken = default)
        {
            var roles = await _roleManager.Roles.ToListAsync(cancellationToken);

            if(roles == null)
            {
                return BadRequest("Unable to get roles.");
            }

            return Ok(roles);
        }

        [AllowAnonymous]
        [HttpGet("{roleId}")]
        public async Task<IActionResult> GetRoleById(string? roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);

            if(role == null)
            {
                return BadRequest("Unable to get the role.");
            }

            return Ok(role);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateCustomRole([FromBody] RoleModel? roleModel)
        {
            if(roleModel == null)
            {
                return BadRequest($"{nameof(roleModel)} cannot be null.");
            }

            var role = _mapper.Map<IdentityRole>(roleModel);

            IdentityResult result = await _roleManager.CreateAsync(role);

            if (!result.Succeeded)
            {
                return BadRequest("Unable to create a role.");
            }

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost]
        // These controller creates roles and their users in advance
        public async Task<IActionResult> CreateRolesAndUsers()
        {
            // Checking if admin role already exists
            bool exists = await _roleManager.RoleExistsAsync("Admin");

            if (!exists)
            {
                // First we are creating Admin role
                var role = new IdentityRole();
                role.Name = "Admin";

                await _roleManager.CreateAsync(role);

                // Then we are creating User that will have a Admin role
                var user = new User();

                string email = Configuration["AdminSettings:Email"];
                string password = Configuration["AdminSettings:Password"];

                user.FirstName = Configuration["AdminSettings:FirstName"]; ;
                user.LastName = Configuration["AdminSettings:LastName"];
                user.UserName = user.Email = email;
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, password);

                IdentityResult result = await _userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, role.Name);
                }else
                {
                    return BadRequest("Unable to create a Admin role.");
                }
            }

            // Checking if Manager role already exists
            exists = await _roleManager.RoleExistsAsync("Manager");

            if (!exists)
            {
                // First we are creating Manager role
                var role = new IdentityRole();
                role.Name = "Manager";

                await _roleManager.CreateAsync(role);

                // Then we are creating User that will have a Manager role
                var user = new User();

                string email = Configuration["ManagerSettings:Email"];
                string password = Configuration["ManagerSettings:Password"];

                user.FirstName = Configuration["ManagerSettings:FirstName"];
                user.LastName = Configuration["ManagerSettings:LastName"];
                user.UserName = user.Email = email;
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, password);

                IdentityResult result = await _userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, role.Name);
                }else
                {
                    return BadRequest("Unable to create a Manager role.");
                }

            }


            // Checking if Manager role already exists
            exists = await _roleManager.RoleExistsAsync("User");

            if (!exists)
            {
                // First we are creating Manager role
                var role = new IdentityRole();
                role.Name = "User";

                IdentityResult result = await _roleManager.CreateAsync(role);

                if (!result.Succeeded)
                {
                    return BadRequest("Unable to create a User role.");
                }

            }

            return Ok();
        }

        [HttpDelete("{roleId}")]
        public async Task<IActionResult> DeleteRole(string? roleId)
        {
            if (string.IsNullOrEmpty(roleId))
            {
                return BadRequest($"{nameof(roleId)} cannot be null or empty.");
            }

            var role = await _roleManager.FindByIdAsync(roleId);

            IdentityResult result = await _roleManager.DeleteAsync(role);

            if (!result.Succeeded)
            {
                return BadRequest("Unable to delete the role.");
            }

            return Ok();
        }
    }
}
