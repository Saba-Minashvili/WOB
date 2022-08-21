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
                .MaximumLength(200)
                .WithMessage("Comment should be maximum 200 characters long.");
        }
    }
}
