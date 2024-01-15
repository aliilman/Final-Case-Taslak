using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MOS.Data.Entity
{

    [Table("Payment", Schema = "dbo")]
    public class Payment
    {
        public int PaymentId { get; set; }
        public int ExpenseId { get; set; }
        public virtual Expense Expense { get; set; }

        public string IBAN { get; set; }
        public decimal PaymentAmount { get; set; }
        public string Description { get; set; }
        public string PaymentType { get; set; }

        public DateTime ExpenceDate { get; set; }

    }

    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.Property(x => x.PaymentId).ValueGeneratedOnAdd();

            builder.Property(x => x.PaymentId).IsRequired(true);
            builder.Property(x => x.ExpenseId).IsRequired(true);
            builder.Property(x => x.IBAN).IsRequired(true).HasMaxLength(34);

            builder.Property(x => x.PaymentAmount).IsRequired(true).HasPrecision(18, 4);
            builder.Property(x => x.Description).IsRequired(true);
            builder.Property(x => x.PaymentType).IsRequired(true);
            builder.Property(x => x.ExpenceDate).IsRequired(true);

            builder.HasIndex(x => x.PaymentId).IsUnique(true);
            builder.HasIndex(x => x.ExpenseId).IsUnique(true);
            builder.HasKey(x => x.PaymentId);



            

        }
    }
}