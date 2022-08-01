using Domain.Authentication;

namespace Persistence.Authentication.Abstraction
{
    public interface IJwtAuthenticationService
    {
        Task<TokenResponse?> Authenticate(TokenRequest? request);
    }
}
