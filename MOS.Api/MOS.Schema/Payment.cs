using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MOS.Schema
{
   public class PaymentRequest
    {
        [JsonIgnore]
        public int PaymentId { get; set; }
        public int ExpenseId { get; set; }
       // public virtual ExpenseResponse Expense { get; set; }

        public string IBAN { get; set; }
        public decimal PaymentAmount { get; set; }
        public string Description { get; set; }
        public string PaymentType { get; set; }

        public DateTime ExpenseDate { get; set; }

    }
    public class PaymentResponse
    {
        public int PaymentId { get; set; }
        public int ExpenseId { get; set; }
        //public virtual ExpenseResponse Expense { get; set; }

        public string IBAN { get; set; }
        public decimal PaymentAmount { get; set; }
        public string Description { get; set; }
        public string PaymentType { get; set; }

        public DateTime ExpenseDate { get; set; }

    }
}