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
                .MaximumLength(100)
                .WithMessage("Comment should be maximum 100 characters long.");
            RuleFor(o => o.Name)
                .NotNull()
                .NotEmpty()
                .Matches("^[a-zA-Z ]*$");
        }
    }
}
