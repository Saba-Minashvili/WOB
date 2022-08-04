using Contracts;
using FluentValidation;

namespace Middleware.Validators
{
    public class ChangeEmailDtoValidator : AbstractValidator<ChangeEmailDto>
    {
        public ChangeEmailDtoValidator()
        {
            RuleFor(o => o.UserId)
                .NotNull()
                .NotEmpty();
            RuleFor(o => o.NewEmail)
                .NotNull()
                .NotEmpty()
                .EmailAddress()
                .Matches(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
    }
}
