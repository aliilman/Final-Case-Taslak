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
                .NotEmpty().WithMessage("IBAN boş bırakılamaz.")
                .Length(26).WithMessage("IBAN 26 karakter olmalıdır.");

            RuleFor(x => x.PaymentAmount)
                .GreaterThan(0).WithMessage("Ödeme miktarı 0'dan büyük olmalıdır.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Açıklama boş bırakılamaz.")
                .MaximumLength(200).WithMessage("Açıklama en fazla 200 karakter olmalıdır.");

            RuleFor(x => x.PaymentType)
                .NotEmpty().WithMessage("Ödeme türü boş bırakılamaz.")
                .MaximumLength(30).WithMessage("Ödeme türü en fazla 30 karakter olmalıdır.");

            RuleFor(x => x.ExpenseDate)
                .NotEmpty().WithMessage("Harcama tarihi boş bırakılamaz.")
                .Must(BeAValidDate).WithMessage("Geçerli bir tarih giriniz.");
        }
        private bool BeAValidDate(DateTime date)
        {
            return date.Date >= DateTime.Now.Date;
        }
    }
}