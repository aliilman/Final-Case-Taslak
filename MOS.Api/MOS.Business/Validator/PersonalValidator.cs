using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MOS.Schema;

namespace MOS.Business.Validator
{
    public class PersonalValidator : AbstractValidator<PersonalRequest>
    {
        public PersonalValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .MinimumLength(3).WithMessage("UserName can be at least 3 characters")
                .MaximumLength(50).WithMessage("UserName can be a maximum of 50 characters")
                .Matches("^[a-zA-Z0-9]*$").WithMessage("UserName must contain only letters and numbers.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6).WithMessage("Password can be at least 6 characters");

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(50).WithMessage("FirstName can be a maximum of 50 characters");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(50).WithMessage("Surname can be a maximum of 50 characters");

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress().WithMessage("Please enter a valid e-mail address.")
                .MaximumLength(100).WithMessage("Email address can be up to 100 characters");
                
            RuleFor(x => x.IBAN).NotEmpty().Length(26).WithMessage("IBAN must be 26 characters.");

        }
    }
}