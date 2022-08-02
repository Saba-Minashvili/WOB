using Domain.Authentication;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Persistence.Authentication.Abstraction;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Persistence.Authentication
{
    public class JwtAuthenticationService : IJwtAuthenticationService
    {
        private readonly SignInManager<User>? _signInManager;
        private readonly UserManager<User>? _userManager;
        private readonly Token? _token;

        public JwtAuthenticationService(
            SignInManager<User>? signInManager, 
            UserManager<User>? userManager,
            IOptions<Token> tokenOptions)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _token = tokenOptions.Value;
        }

        public async Task<TokenResponse?> Authenticate(TokenRequest? request)
        {
            if (await IsValidUser(request.Email, request.Password))
            {
                User user = await GetUserByEmail(request.Email);

                if (user != null)
                {
                    var result = GenerateJwtToken(user);

                    await _userManager.UpdateAsync(user);

                    return new TokenResponse() { Token = result.Item1, ExpireAt = result.Item2, UserId = user.Id };
                }
            }
            else
            {
                throw new InvalidCredentialsException();
            }

            return null;
        }

        private async Task<bool> IsValidUser(string? email, string? password)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException(nameof(email));
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(nameof(password));
            }

            User? user = await GetUserByEmail(email);

            if (user == null)
            {
                // Username or password was incorrect
                return false;
            }

            SignInResult signInResult = await _signInManager.PasswordSignInAsync(user, password, true, false);

            return signInResult.Succeeded;
        }

        private async Task<User> GetUserByEmail(string? email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        private Tuple<string?, DateTime?> GenerateJwtToken(User? user)
        {
#pragma warning disable CS8604 // Possible null reference argument.
            byte[] secret = Encoding.ASCII.GetBytes(_token.Secret);
#pragma warning restore CS8604 // Possible null reference argument.

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
#pragma warning disable CS8604 // Possible null reference argument.
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Issuer = _token.Issuer,
                Audience = _token.Audience,
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId", user.Id),
                    new Claim(ClaimTypes.Email, user.Email),
                    // Getting role for current user
                    new Claim(ClaimTypes.Role, _userManager.GetRolesAsync(user).Result.FirstOrDefault())
                }),

                Expires = DateTime.UtcNow.AddMinutes(_token.Expiry),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
            };
#pragma warning restore CS8604 // Possible null reference argument.

            SecurityToken securityToken = handler.CreateToken(descriptor);
            string jwtToken = handler.WriteToken(securityToken);

            return new Tuple<string?, DateTime?>(jwtToken, descriptor.Expires);
        }
    }
}
