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
            RuleFor(x => x.UserName).NotEmpty().MinimumLength(5).MaximumLength(50);
            RuleFor(x => x.Password).NotEmpty().MinimumLength(5).MaximumLength(50);
        }
    }
}