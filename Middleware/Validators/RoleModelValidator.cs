using Contracts.ViewModels;
using FluentValidation;

namespace Middleware.Validators
{
    public class RoleModelValidator : AbstractValidator<RoleModel>
    {
        public RoleModelValidator()
        {
            RuleFor(o => o.Name)
                .NotNull()
                .NotEmpty()
                .Matches("^[a-zA-Z ]*$");
        }
    }
}
