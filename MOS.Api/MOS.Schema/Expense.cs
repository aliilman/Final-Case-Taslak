using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MOS.Base.Enum;


namespace MOS.Schema
{
    public class PersonalExpenseRequest
    {
        [JsonIgnore]
        public int ExpenseId { get; set; }
        [JsonIgnore]
        public int PersonalNumber { get; set; }
        // public virtual Personal Personal { get; set; }

        // public ApprovalStatus ApprovalStatus { get; set; }
        // public DateTime ExpenseCreateDate { get; set; } // harcama zamanı

        public string ExpenseName { get; set; }
        public string ExpenseCategory { get; set; }

        public decimal ExpenseAmount { get; set; } //tutar
        public string ExpenseDescription { get; set; } // harcama Açıklaması
        public string InvoiceImageFilePath { get; set; } //fiş veya fatura resmi yüklenmiş kabul edildi
        public string? Location { get; set; }

        // public int? DeciderAdminNumber { get; set; }=null;
        // public virtual Admin Admin { get; set; }
        // public string? DecisionDescription { get; set; }
        // public DateTime? DecisionDate { get; set; }

        // public Payment? Payment { get; set; }
    }
    public class ExpenseResponse
    {
        public int ExpenseId { get; set; }

        public ApprovalStatus ApprovalStatus { get; set; }
        public DateTime ExpenseCreateDate { get; set; } // harcama zamanı
        public decimal ExpenseAmount { get; set; } //tutar
        public string ExpenseDescription { get; set; } // harcama Açıklaması
        public string InvoiceImageFilePath { get; set; } //fiş veya fatura resmi yüklenmiş kabul edildi
        public string? Location { get; set; }

        public string? DecisionDescription { get; set; }
        public DateTime? DecisionDate { get; set; }

        public PaymentResponse? Payment { get; set; }
    }
    public class AdminExpenseRequest
    {
        //public int ExpenseId { get; set; }
        public string DecisionDescription { get; set; }

    }


}