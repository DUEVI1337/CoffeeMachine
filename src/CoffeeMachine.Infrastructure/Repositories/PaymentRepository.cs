using CoffeeMachine.Domain.Entities;

namespace CoffeeMachine.Infrastructure.Repositories
{
    public class PaymentRepository : BaseRepository<Payment>
    {
        public PaymentRepository(DataContext db) : base(db)
        {
        }
    }
}