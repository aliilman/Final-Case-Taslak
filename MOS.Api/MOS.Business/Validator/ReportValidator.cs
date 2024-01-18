using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MOS.Schema;

namespace MOS.Business.Validator
{
    public class ReportValidator : AbstractValidator<ReportRequest>
    {
        public ReportValidator()
        {
            RuleFor(x => x)
            .Must(x => x.StartExpenceDate < x.EndExpenceDate)
            .WithMessage("The StartExpenceDate must be less than the enddate.")
            .Must(x => x.StartExpenceDate < DateTime.Now)
            .WithMessage("The StartExpenceDate must be less than the DateTime.NOW.");

            //içeriklerin boş geliği durumlar için senaryolar geliştirilmiştir. O yüzden konraol edilmemiştir.   
        }       
    }
}