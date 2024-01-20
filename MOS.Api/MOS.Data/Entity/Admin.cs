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


            builder.Property(x => x.AdminNumber).IsRequired(true);


            builder.Property(x => x.UserName).IsRequired(true).HasMaxLength(100);
            builder.Property(x => x.Password).IsRequired(true).HasMaxLength(50);
            builder.Property(x => x.FirstName).IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.LastName).IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.Email).IsRequired(false).HasMaxLength(100); ;

            builder.HasIndex(x => x.AdminNumber).IsUnique(true);

            builder.HasKey(x => x.AdminNumber);

        }

    }
}