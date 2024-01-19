using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOS.Schema
{
    public class ReportRequest
    {
        public DateTime? StartExpenceDate { get; set; } // harcama tarih aralığı için başlangıç tarihi
        public DateTime? EndExpenceDate { get; set; }// harcama tarih araliği için bitiş tarihi
        public List<int> PersonalNumberList { get; set; } // İstenen Personaller
    }
    public class ReportRequestForOnePersonal
    {
        public DateTime? StartExpenceDate { get; set; } // harcama tarih aralığı için başlangıç tarihi
        public DateTime? EndExpenceDate { get; set; }// harcama tarih araliği için bitiş tarihi
    }
    public class ReportResponse
    {
        public string RaporName { get; set; }// rapor adı

        public DateTime ReportCrateDate { get; set; } // rapor oluşturma tarihi
        public DateTime StartTheDate { get; set; } // raporda listelenen verilerin tarih aralığı //başlangıç
        public DateTime EndTheDate { get; set; }// bitiş

        public decimal TotalMoneySpentByPersonals { get; set; }// toplam harcanan
        public decimal TotalApprovedSpentMoney { get; set; } // toplam onaylanan
        public decimal TotalRejectedSpentMoney { get; set; } // toplan reddedilen

        public List<ReportEachPersonal> ReportEachPersonalList { get; set; }

    }
    public class ReportEachPersonal // her perosnal için ayrı listeme
    {
        public PersonalResponse PersonalResponse { get; set; } //personal bilgileri
        public decimal MoneySpentByPersonal { get; set; } //personalin toplam harcaması
        public decimal ApprovedSpentMoney { get; set; }// personalin toplam onaylanan
        public decimal RejectedSpentMoney { get; set; } // personalin toplam reddedilen
        public List<ExpenseResponse> WaitingExpenseList { get; set; } //beliritlen tarihler arasında beklemde olan
        public List<ExpenseResponse> AproveedExpenseList { get; set; }// belirtilen tarihler arasında onaylanmış olan
        public List<PaymentResponse> PaymentList { get; set; } // belirtilen tarihler arasında oluşturulmuş olan 
        public List<ExpenseResponse> RejecetExpenseList { get; set; } // belirtilen tarihler arasında reddilmiş olan
    }
}