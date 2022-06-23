using CoffeeMachine.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoffeeMachine.Infrastructure
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> opt) : base(opt)
        {
            
        }

        public DbSet<Coffee> Coffees { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<BanknoteCashbox> BanknoteCashboxes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Order>().HasKey(x => new { x.CoffeeId, x.PaymentId });
            builder.Entity<Coffee>().HasData(
                new Coffee[]
                {
                    new Coffee { CoffeeId = 1, Name = "Капучино", Price = 600},
                    new Coffee { CoffeeId = 2, Name = "Латте", Price = 850},
                    new Coffee { CoffeeId = 3, Name = "Американо", Price = 900}
                });
            builder.Entity<BanknoteCashbox>().HasData(
                new BanknoteCashbox[]
                {
                    new BanknoteCashbox { BanknoteId = 1, Denomination = 50, CountBanknote = 50},
                    new BanknoteCashbox { BanknoteId = 2, Denomination = 100, CountBanknote = 40},
                    new BanknoteCashbox { BanknoteId = 3, Denomination = 200, CountBanknote = 30},
                    new BanknoteCashbox { BanknoteId = 4, Denomination = 500, CountBanknote = 20},
                    new BanknoteCashbox { BanknoteId = 5, Denomination = 1000, CountBanknote = 15},
                    new BanknoteCashbox { BanknoteId = 6, Denomination = 2000, CountBanknote = 10},
                    new BanknoteCashbox { BanknoteId = 7, Denomination = 5000, CountBanknote = 5},
                });
        }
    }
}
