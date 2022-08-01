using Domain.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.Authentication.Abstraction;

namespace WOB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IJwtAuthenticationService? _jwtAuth;

        public AuthenticationController(IJwtAuthenticationService jwtAuth) => _jwtAuth = jwtAuth;

        [AllowAnonymous]
        [HttpPost]
        public async Task<TokenResponse?> AuthenticateAsync([FromBody] TokenRequest request)
        {
            return await _jwtAuth.Authenticate(request);
        }
    }
}
