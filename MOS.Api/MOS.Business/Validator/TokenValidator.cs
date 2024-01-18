using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MOS.Schema;

namespace MOS.Business.Validator
{
    public class TokenValidator : AbstractValidator<TokenRequest>
    {
        public TokenValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Username is mus be not null!!")
            .MinimumLength(5).MaximumLength(50);
            RuleFor(x => x.Password).NotEmpty().WithMessage("Username is mus be not null!!")
            .MinimumLength(5).MaximumLength(50);
        }
    }
}