using CoffeeMachine.Domain.Entities;

namespace CoffeeMachine.Infrastructure.Repositories
{
    public class IncomeRepository : BaseRepository<Income>
    {
        public IncomeRepository(DataContext db) : base(db)
        {
        }
    }
}