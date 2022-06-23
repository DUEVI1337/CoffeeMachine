using CoffeeMachine.Core.Entities;

namespace CoffeeMachine.Infrastructure.Repositories
{
    /// <summary>
    /// repository for work with entities <see cref="Coffee"/>
    /// </summary>
    public class CoffeeRepository : BaseRepository<Coffee>
    {
        public CoffeeRepository(DataContext db) : base(db)
        {
        }
    }
}