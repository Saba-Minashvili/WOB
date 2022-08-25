using Contracts.FeedBack;
using FluentValidation;

namespace Middleware.Validators
{
    public sealed class UpdateFeedBackDtoValidator : AbstractValidator<UpdateFeedBackDto>
    {
        public UpdateFeedBackDtoValidator()
        {
            RuleFor(o => o.Comment)
                .NotNull()
                .NotEmpty()
                .Matches("^[a-zA-Z0-9 ]*$")
                .MaximumLength(600)
                .WithMessage("Comment should be maximum 600 characters long.");
            RuleFor(o => o.ModifiedAt)
                .NotNull()
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}
