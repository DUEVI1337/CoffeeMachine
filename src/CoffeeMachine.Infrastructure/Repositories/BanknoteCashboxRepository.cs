using CoffeeMachine.Domain.Entities;

namespace CoffeeMachine.Infrastructure.Repositories
{
    /// <summary>
    /// repository for work with entities <see cref="BanknoteCashBox"/>
    /// </summary>
    public class BanknoteCashboxRepository : BaseRepository<BanknoteCashBox>
    {
        public BanknoteCashboxRepository(DataContext db) : base(db)
        {
        }
    }
}