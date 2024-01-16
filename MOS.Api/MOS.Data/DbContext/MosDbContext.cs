using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MOS.Data.Entity;


namespace MOS.Data;
public class MosDbContext : DbContext
{
    public MosDbContext(DbContextOptions<MosDbContext> options) : base(options)
    {

    }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Personal> Personals { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AdminConfiguration());
        modelBuilder.ApplyConfiguration(new ExpenseConfiguration());
        modelBuilder.ApplyConfiguration(new PaymentConfiguration());
        modelBuilder.ApplyConfiguration(new PersonalConfiguration());

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Admin>().HasData(
            new Admin() 
            { 
                AdminNumber = 1,
                FirstName = "Ali", LastName = "İlman",
                Email = "ali.ilman@akbank.com", Password = "Admin",
                UserName = "aliilman"
            });
        modelBuilder.Entity<Admin>().HasData(
            new Admin() 
            {
                AdminNumber = 2,
                FirstName = "Veli", LastName = "liman",
                Email = "veli.liman@akbank.com", Password = "Admin",
                UserName = "veliliman"
            });

        modelBuilder.Entity<Personal>().HasData(
            new Personal()
            {
                PersonalNumber = 1,
                FirstName = "Ferdi",
                LastName = "Kadi",
                Email = "ferdi.kadi@akbank.com",
                Password = "personal",
                UserName = "ferdikadi",
                IBAN = "12345678981234456798",
            });
        modelBuilder.Entity<Personal>().HasData(
            new Personal()
            {
                PersonalNumber = 2,
                FirstName = "Arda",
                LastName = "Gul",
                Email = "Arda.gul@akbank.com",
                Password = "personal",
                UserName = "ardagul",
                IBAN = "56412345678981234456798",
            });
        modelBuilder.Entity<Personal>().HasData(
            new Personal()
            {
                PersonalNumber = 3,
                FirstName = "Sebastian",
                LastName = "simanski",
                Email = "sebastian.simanski@akbank.com",
                Password = "personal",
                UserName = "sebastiansimanski",
                IBAN = "1233456789856451234456798",
            });
        modelBuilder.Entity<Personal>().HasData(
            new Personal()
            {
                PersonalNumber = 4,
                FirstName = "Edin",
                LastName = "Ceko",
                Email = "edin.ceko@akbank.com",
                Password = "personal",
                UserName = "edinceko",
                IBAN = "12345678981541654234456798",
            });

    }


}