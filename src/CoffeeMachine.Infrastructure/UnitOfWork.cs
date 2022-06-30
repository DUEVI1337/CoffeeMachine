using System.Threading.Tasks;

using CoffeeMachine.Domain.Entities;
using CoffeeMachine.Domain.Interfaces.Repositories;
using CoffeeMachine.Infrastructure.Repositories;

namespace CoffeeMachine.Infrastructure
{
    /// <summary>
    /// unite all repository together 
    /// </summary>
    public class UnitOfWork
    {
        private readonly DataContext _db;

        public UnitOfWork(DataContext db, CoffeeRepository coffeeRepo, BalanceRepository balanceRepo,
            BanknoteCashboxRepository banknoteCashboxRepo, PaymentRepository paymentRepo, IncomeRepository incomeRepo)
        {
            _db = db;
            CoffeeRepo = coffeeRepo;
            BalanceRepo = balanceRepo;
            BanknoteCashboxRepo = banknoteCashboxRepo;
            PaymentRepo = paymentRepo;
            IncomeRepo = incomeRepo;
        }

        /// <summary>
        /// property for DI (ability use basic methods for work with entity <see cref="Balance"/> in database)
        /// </summary>
        public IRepository<Balance> BalanceRepo { get; set; }

        /// <summary>
        /// property for DI (ability use basic methods for work with entity <see cref="Balance"/> in database)
        /// </summary>
        public IRepository<BanknoteCashbox> BanknoteCashboxRepo { get; set; }

        /// <summary>
        /// property for DI (ability use basic methods for work with entity <see cref="Coffee"/> in database)
        /// </summary>
        public IRepository<Coffee> CoffeeRepo { get; set; }

        /// <summary>
        /// property for DI (ability use basic methods for work with entity <see cref="Coffee"/> in database)
        /// </summary>
        public IRepository<Income> IncomeRepo { get; set; }

        /// <summary>
        /// property for DI (ability use basic methods for work with entity <see cref="Coffee"/> in database)
        /// </summary>
        public IRepository<Payment> PaymentRepo { get; set; }

        /// <summary>
        /// save changes in database after something action with entity in database
        /// </summary>
        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}