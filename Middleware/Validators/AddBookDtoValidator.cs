﻿using Contracts.Book;
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
                .MinimumLength(10)
                .MaximumLength(70)
                .WithMessage("Title should be at least 10 and maximum 70 characters long.");
            RuleFor(o => o.Image)
                .NotNull()
                .NotEmpty();
            RuleFor(o => o.Authors)
                .NotNull()
                .NotEmpty();
            RuleFor(o => o.Genres)
                .NotNull()
                .NotEmpty();
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
                .NotEmpty();
        }
    }
}
