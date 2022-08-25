using Contracts.FeedBack;
using FluentValidation;

namespace Middleware.Validators
{
    public sealed class AddFeedBackDtoValidator : AbstractValidator<AddFeedBackDto>
    {
        public AddFeedBackDtoValidator()
        {
            RuleFor(o => o.Comment)
                .NotNull()
                .NotEmpty()
                .MaximumLength(600)
                .WithMessage("Comment should be maximum 600 characters long.");
            RuleFor(o => o.UserName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50)
                .Matches("^[a-zA-Z ]*$");
        }
    }
}
