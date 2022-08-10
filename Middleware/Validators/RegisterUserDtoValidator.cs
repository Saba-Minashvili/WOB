﻿using Contracts.User;
using FluentValidation;

namespace Middleware.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator()
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
            RuleFor(o => o.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress()
                .Matches(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            RuleFor(o => o.Password)
                .NotNull()
                .NotEmpty()
                .Matches("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")
                .MinimumLength(8)
                .MaximumLength(16);
        }
    }
}
