using CoffeeMachine.Domain.Entities;

namespace CoffeeMachine.Infrastructure.Repositories
{
    /// <summary>
    /// repository for work with entities <see cref="BanknoteCashbox"/>
    /// </summary>
    public class BanknoteCashboxRepository : BaseRepository<BanknoteCashbox>
    {
        public BanknoteCashboxRepository(DataContext db) : base(db)
        {
        }
    }
}