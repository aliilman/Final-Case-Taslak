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
                .NotEmpty().WithMessage("Kullanıcı adı boş bırakılamaz.")
                .MinimumLength(3).WithMessage("Kullanıcı adı en az 3 karakter olmalıdır.")
                .MaximumLength(50).WithMessage("Kullanıcı adı en fazla 50 karakter olmalıdır.")
                .Matches("^[a-zA-Z0-9]*$").WithMessage("Kullanıcı adı sadece harf ve rakam içermelidir.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Parola boş bırakılamaz.")
                .MinimumLength(6).WithMessage("Parola en az 6 karakter olmalıdır.");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Ad boş bırakılamaz.")
                .MaximumLength(50).WithMessage("Ad en fazla 50 karakter olmalıdır.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Soyad boş bırakılamaz.")
                .MaximumLength(50).WithMessage("Soyad en fazla 50 karakter olmalıdır.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-posta adresi boş bırakılamaz.")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.")
                .MaximumLength(100).WithMessage("E-posta adresi en fazla 100 karakter olmalıdır.");
            RuleFor(x => x.IBAN).NotEmpty().Length(26);

        }
    }
}