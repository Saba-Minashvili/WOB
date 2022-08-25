using Contracts.Book;
using FluentValidation;

namespace Middleware.Validators
{
    public class AddBookDtoValidator : AbstractValidator<AddBookDto>
    {
        public AddBookDtoValidator()
        {
            RuleFor(o => o.Name)
                .NotNull()
                .NotEmpty()
                .Matches("^[a-zA-Z ]*$")
                .MaximumLength(100)
                .WithMessage("Title should be at least 10 and maximum 70 characters long.");
            RuleFor(o => o.Image)
                .NotNull()
                .NotEmpty();
            RuleForEach(o => o.Authors)
                .ChildRules(m =>
                {
                    m.RuleFor(c => c.FirstName)
                        .NotNull()
                        .NotEmpty()
                        .Matches("^[a-zA-Z ]*$")
                        .MaximumLength(50);
                    m.RuleFor(c => c.LastName)
                        .NotNull()
                        .NotEmpty()
                        .Matches("^[a-zA-Z ]*$")
                        .MaximumLength(50);
                    m.RuleFor(c => c.Age)
                        .NotNull()
                        .NotEmpty()
                        .NotEqual(0)
                        .WithMessage("Age cannot be equal to 0.");
                    m.RuleFor(c => c.Origin)
                        .NotNull()
                        .NotEmpty()
                        .MaximumLength(100);
                    m.RuleFor(c => c.Biography)
                        .NotNull()
                        .NotEmpty()
                        .MaximumLength(600)
                        .WithMessage("Biography must be maximum of 600 characters long.");
                    m.RuleFor(c => c.DateOfBirth)
                        .NotNull()
                        .NotEmpty()
                        .MaximumLength(100);
                });
            RuleForEach(o => o.Genres)
                .ChildRules(m =>
                {
                    m.RuleFor(c => c.GenreName)
                        .NotNull()
                        .NotEmpty()
                        .MaximumLength(100);
                });
            RuleFor(o => o.Pages)
                .NotEqual(0)
                .NotEmpty()
                .WithMessage("Number of pages cannot be equal to zero.");
            RuleFor(o => o.Description)
                .NotNull()
                .NotEmpty()
                .MaximumLength(600)
                .WithMessage("Description should be maximum 600 characters long.");
            RuleFor(o => o.ReleaseDate)
                .NotNull()
                .NotEmpty()
                .Length(4)
                .WithMessage("Release Date must be 4 characters long.");
        }
    }
}
