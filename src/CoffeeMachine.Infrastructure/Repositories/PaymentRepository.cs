using CoffeeMachine.Domain.Entities;

namespace CoffeeMachine.Infrastructure.Repositories
{
    /// <summary>
    /// repository for work with entities <see cref="Payment"/>
    /// </summary>
    public class PaymentRepository : BaseRepository<Payment>
    {
        public PaymentRepository(DataContext db) : base(db)
        {
        }
    }
}