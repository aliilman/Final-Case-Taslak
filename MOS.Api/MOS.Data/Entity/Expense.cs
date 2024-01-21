using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MOS.Base.Enum;

namespace MOS.Data.Entity
{
    [Table("Expense", Schema = "dbo")]
    public class Expense
    {
        public int ExpenseId { get; set; }

        public int PersonalNumber { get; set; }
        public virtual Personal Personal { get; set; }

        public string ExpenseName { get; set; }
        public string ExpenseCategory { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; } //Onaylanma durumu, saved =1, approve=2, rejecd = 3
        public DateTime ExpenseCreateDate { get; set; } // harcama zamanı
        public decimal ExpenseAmount { get; set; } //tutar
        public string ExpenseDescription { get; set; } // harcama Açıklaması
        public string InvoiceImageFilePath { get; set; } //fiş veya fatura resmi yüklenmiş kabul edildi
        public string? Location { get; set; }

        public int? DeciderAdminNumber { get; set; }=null;
        public virtual Admin? Admin { get; set; }
        public string? DecisionDescription { get; set; }
        public DateTime? DecisionDate { get; set; }

        public Payment? Payment { get; set; }//onaylanmış ise payment tanımlanır
    }

    public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
    {
        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.Property(x => x.ExpenseId).ValueGeneratedOnAdd();

            builder.Property(x => x.ExpenseId).IsRequired(true);
            builder.Property(x => x.ExpenseName).IsRequired(true).HasMaxLength(50);
            builder.Property(x => x.ExpenseCategory).IsRequired(true).HasMaxLength(50);
            
            builder.Property(x => x.PersonalNumber).IsRequired(true);
            builder.Property(x => x.ApprovalStatus).IsRequired(true);
            builder.Property(x => x.ExpenseCreateDate).IsRequired(true);
            builder.Property(x => x.ExpenseAmount).IsRequired(true).HasPrecision(18, 4);
            builder.Property(x => x.ExpenseDescription).IsRequired(true).HasMaxLength(100);
            builder.Property(x => x.InvoiceImageFilePath).IsRequired(true).HasMaxLength(150);

            builder.Property(x => x.Location).IsRequired(false).HasMaxLength(150);

            builder.Property(x => x.DeciderAdminNumber).IsRequired(false);
            builder.Property(x => x.DecisionDescription).IsRequired(false);
            builder.Property(x => x.DecisionDate).IsRequired(false);


            builder.HasIndex(x => x.PersonalNumber);

            builder.HasIndex(x => x.ExpenseId).IsUnique(true);
            builder.HasKey(x => x.ExpenseId);

            builder.HasOne(x => x.Payment)
            .WithOne(n=>n.Expense)
            .HasForeignKey<Payment>(p => p.ExpenseId);

        }
    }
}