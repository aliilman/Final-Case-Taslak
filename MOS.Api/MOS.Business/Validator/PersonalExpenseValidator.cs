using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MOS.Schema;

namespace MOS.Business.Validator
{
    public class PersonalExpenseValidator : AbstractValidator<PersonalExpenseRequest>
    {
        public PersonalExpenseValidator()
        {
            RuleFor(x => x.ExpenseName)
                .NotEmpty()
                .MaximumLength(50).WithMessage("ExpenseName can be up to 50 characters");

            RuleFor(x => x.ExpenseCategory)
                .NotEmpty()
                .MaximumLength(50).WithMessage("ExpenseCategory name can be up to 50 characters");

            RuleFor(x => x.ExpenseAmount)
                .GreaterThan(0).WithMessage("ExpenseAmount must be greater than 0.");

            RuleFor(x => x.ExpenseDescription)
                .NotEmpty()
                .MaximumLength(100).WithMessage("ExpenseDescription can be up to 100 characters");

            RuleFor(x => x.InvoiceImageFilePath)
                .NotEmpty()
                .MaximumLength(150).WithMessage("InvoiceImageFilePath can be up to 150 characters");

            RuleFor(x => x.Location)
                .MaximumLength(150).WithMessage("Location can be up to 150 characters");
        }
    }
}