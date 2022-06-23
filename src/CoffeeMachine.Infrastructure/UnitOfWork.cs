using System.Threading.Tasks;

using CoffeeMachine.Core.Entities;
using CoffeeMachine.Core.Interfaces.Repositories;
using CoffeeMachine.Infrastructure.Repositories;

namespace CoffeeMachine.Infrastructure
{
    /// <summary>
    /// unite all repository together 
    /// </summary>
    public class UnitOfWork
    {
        private readonly DataContext _db;

        public UnitOfWork(DataContext db, CoffeeRepository coffeeRepo)
        {
            _db = db;
            CoffeeRepo = coffeeRepo;
        }

        /// <summary>
        /// property for DI (ability use basic methods for work with entity <see cref="Coffee"/> in database)
        /// </summary>
        public IRepository<Coffee> CoffeeRepo { get; set; }

        /// <summary>
        /// save changes in database after something action with entity in database
        /// </summary>
        public async Task SaveChanges()
        {
            await _db.SaveChangesAsync();
        }
    }
}