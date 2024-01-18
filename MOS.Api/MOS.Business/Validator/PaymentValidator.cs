using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MOS.Schema;

namespace MOS.Business.Validator
{
    public class PaymentValidator : AbstractValidator<PaymentRequest>
    {
        public PaymentValidator()
        {
            RuleFor(x => x.IBAN)
                .NotEmpty()
                .Length(26).WithMessage("IBAN must be 26 characters");

            RuleFor(x => x.PaymentAmount)
                .GreaterThan(0).WithMessage("PaymentAmount must be greater than 0");

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(200).WithMessage("Description must be at most 200 characters.");

            RuleFor(x => x.PaymentType)
                .NotEmpty()
                .MaximumLength(30).WithMessage("PaymentType must be at most 30 characters.");

            RuleFor(x => x.ExpenseDate)
                .NotEmpty();
                
        }

    }
}