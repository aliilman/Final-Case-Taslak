using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MOS.Base.Entity;

namespace MOS.Data.Entity
{
    [Table("Admin", Schema = "dbo")]
    public class Admin : BaseEmployee
    {
        public int AdminNumber { get; set; }

    }
    public class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.Property(x => x.UserId).ValueGeneratedNever();//patladı
            
            builder.Property(x => x.AdminNumber).IsRequired(true);

            builder.Property(x => x.UserId).IsRequired(true);
            builder.Property(x => x.UserName).IsRequired(true);
            builder.Property(x => x.Password).IsRequired(true);
            builder.Property(x => x.FirstName).IsRequired(false);
            builder.Property(x => x.LastName).IsRequired(false);
            builder.Property(x => x.Email).IsRequired(false);
            //builder.Property(x => x.Role).IsRequired(false).HasDefaultValue("Personal");
            //builder.Property(x => x.IsActive).IsRequired(true).HasDefaultValue(true);

            builder.HasIndex(x => x.AdminNumber).IsUnique(true);
           // builder.HasIndex(x => x.UserId).IsUnique(true);
            builder.HasKey(x => x.AdminNumber);

        }

    }
}