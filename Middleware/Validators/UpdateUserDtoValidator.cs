using Contracts.User;
using FluentValidation;

namespace Middleware.Validators
{
    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator()
        {
            RuleFor(o => o.FirstName)
                .NotNull()
                .NotEmpty()
                .Matches("^[a-zA-Z ]*$")
                .MaximumLength(50)
                .WithMessage("FirstName cannot exceed 50 characters.");
            RuleFor(o => o.LastName)
                .NotNull()
                .NotEmpty()
                .Matches("^[a-zA-Z ]*$")
                .MaximumLength(50)
                .WithMessage("LastName cannot exceed 50 characters.");
        }
    }
}
