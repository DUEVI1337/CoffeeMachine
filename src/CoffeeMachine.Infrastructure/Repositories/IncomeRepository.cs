using CoffeeMachine.Domain.Entities;

namespace CoffeeMachine.Infrastructure.Repositories
{
    /// <summary>
    /// repository for work with entities <see cref="Income"/>
    /// </summary>
    public class IncomeRepository : BaseRepository<Income>
    {
        public IncomeRepository(DataContext db) : base(db)
        {
        }
    }
}