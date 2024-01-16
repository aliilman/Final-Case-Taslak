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
                .NotEmpty().WithMessage("Harcama adı boş bırakılamaz.")
                .MaximumLength(50).WithMessage("Harcama adı en fazla 50 karakter olmalıdır.");

            RuleFor(x => x.ExpenseCategory)
                .NotEmpty().WithMessage("Harcama kategorisi boş bırakılamaz.")
                .MaximumLength(50).WithMessage("Harcama kategorisi en fazla 50 karakter olmalıdır.");

            RuleFor(x => x.ExpenseAmount)
                .GreaterThan(0).WithMessage("Harcama tutarı 0'dan büyük olmalıdır.");

            RuleFor(x => x.ExpenseDescription)
                .NotEmpty().WithMessage("Harcama açıklaması boş bırakılamaz.")
                .MaximumLength(100).WithMessage("Harcama açıklaması en fazla 100 karakter olmalıdır.");

            RuleFor(x => x.InvoiceImageFilePath)
                .NotEmpty().WithMessage("Fatura resmi yolu boş bırakılamaz.")
                .MaximumLength(150).WithMessage("Harcama açıklaması en fazla 150 karakter olmalıdır.");
               
            RuleFor(x => x.Location)
                .MaximumLength(150).WithMessage("Konum en fazla 150 karakter olmalıdır.");
        }
    }
}