using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
            .Must(x => Datetimevalidate(x.StartExpenceDate,x.EndExpenceDate))
            .WithMessage(" The StartExpenceDate must be less than the enddate - The StartExpenceDate must be less than the DateTime.NOW.");
            //içeriklerin boş geliği durumlar için senaryolar geliştirilmiştir. O yüzden konraol edilmemiştir.   
        }
        private bool Datetimevalidate(DateTime? StartExpenceDate, DateTime? EndExpenceDate)
        {
            //ikisin de null oluğu durum için işlem yapılabilir durumdadır.
            //ikisinin de null olamdığı durumlarda bu koşullar sağlanmalıdır
            if (!(StartExpenceDate == null && EndExpenceDate == null) &&
             (StartExpenceDate > EndExpenceDate ||
              StartExpenceDate > DateTime.Now.AddMinutes(1) ||
              EndExpenceDate > DateTime.Now.AddMinutes(1)))
            {
                return false;
            }
            return true;
        }
    }
    public class ReportRequestForOnePersonalValidator : AbstractValidator<ReportRequestForOnePersonal>
    {
        public ReportRequestForOnePersonalValidator()
        {
            RuleFor(x => x)
            .Must(x => Datetimevalidate(x.StartExpenceDate,x.EndExpenceDate))
            .WithMessage(" The StartExpenceDate must be less than the enddate - The StartExpenceDate must be less than the DateTime.NOW.");
        }
        private bool Datetimevalidate(DateTime? StartExpenceDate, DateTime? EndExpenceDate)
        {
            //ikisin de null oluğu durum için işlem yapılabilir durumdadır.
            //ikisinin de null olamdığı durumlarda bu koşullar sağlanmalıdır
            if (!(StartExpenceDate == null && EndExpenceDate == null) &&
             (StartExpenceDate > EndExpenceDate ||
              StartExpenceDate > DateTime.Now.AddMinutes(1) ||
              EndExpenceDate > DateTime.Now.AddMinutes(1)))
            {
                return false;
            }
            return true;
        }
    }
}