using CoffeeMachine.Domain.Entities;

namespace CoffeeMachine.Infrastructure.Repositories
{
    /// <summary>
    /// repository for work with entities <see cref="Balance"/>
    /// </summary>
    public class BalanceRepository : BaseRepository<Balance>
    {
        public BalanceRepository(DataContext db) : base(db)
        {
        }
    }
}