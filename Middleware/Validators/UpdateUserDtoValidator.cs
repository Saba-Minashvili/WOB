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
                .MaximumLength(10)
                .WithMessage("FirstName cannot exceed 10 characters.");
            RuleFor(o => o.LastName)
                .NotNull()
                .NotEmpty()
                .Matches("^[a-zA-Z ]*$")
                .MaximumLength(20)
                .WithMessage("LastName cannot exceed 20 characters.");
            RuleFor(o => o.Photo)
                .NotNull()
                .NotEmpty();
        }
    }
}
