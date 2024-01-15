using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MOS.Base.Entity;

namespace MOS.Data.Entity;

[Table("Personal", Schema = "dbo")]
public class Personal : BaseUser
{
    public int PersonalNumber { get; set; }
    public string IBAN { get; set; }

    public virtual List<Expense> Expense { get; set; }

}
public class PersonalConfiguration : IEntityTypeConfiguration<Personal>
{
    public void Configure(EntityTypeBuilder<Personal> builder)
    {
        builder.Property(x => x.UserId).ValueGeneratedNever();//patladı

        builder.Property(x => x.UserId).IsRequired(true);
        builder.Property(x => x.UserName).IsRequired(true);
        builder.Property(x => x.Password).IsRequired(true);
        builder.Property(x => x.FirstName).IsRequired(false);
        builder.Property(x => x.LastName).IsRequired(false);
        builder.Property(x => x.Email).IsRequired(false);
        //builder.Property(x => x.Role).IsRequired(false).HasDefaultValue("Personal");
        //builder.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(true);

        builder.Property(x => x.PersonalNumber).IsRequired(true);
        builder.Property(x => x.IBAN).IsRequired(true).HasMaxLength(34);

        builder.HasIndex(x => x.PersonalNumber).IsUnique(true);
        //builder.HasIndex(x => x.UserId).IsUnique(true);
        builder.HasKey(x => x.PersonalNumber);

        builder.HasMany(x => x.Expense)
            .WithOne(x => x.Personal)
            .HasForeignKey(x => x.PersonalNumber)
            .IsRequired(true);

    }
}