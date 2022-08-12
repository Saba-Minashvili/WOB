using Domain.Authentication;
using FluentValidation;

namespace Middleware.Validators
{
    public class TokenRequestValidator : AbstractValidator<TokenRequest>
    {
        public TokenRequestValidator()
        {
            RuleFor(o => o.Email)
                .NotNull()
                .NotEmpty();
            RuleFor(o => o.Password)
                .NotNull()
                .NotEmpty();
        }
    }
}
