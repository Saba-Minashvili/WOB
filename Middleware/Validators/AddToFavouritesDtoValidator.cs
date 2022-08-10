using Contracts.Book;
using FluentValidation;

namespace Middleware.Validators
{
    public class AddToFavouritesDtoValidator : AbstractValidator<AddToFavouritesDto>
    {
        public AddToFavouritesDtoValidator()
        {
            RuleFor(o => o.BookId)
                .NotEmpty()
                .NotEqual(0)
                .WithMessage("Id cannot be 0");
            RuleFor(o => o.UserId)
                .NotNull()
                .NotEmpty();
        }
    }
}
