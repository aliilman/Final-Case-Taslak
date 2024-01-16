using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MOS.Schema;

namespace MOS.Business.Validator
{
    public class AdminExpenseValidator : AbstractValidator<AdminExpenseRequest>
    {
        public AdminExpenseValidator()
        {
           RuleFor(x => x.DecisionDescription)
                .NotEmpty().WithMessage("Açıklama boş bırakılamaz.")
                .MaximumLength(255).WithMessage("Açıklama en fazla 255 karakter olmalıdır.");
        }
    }
}