using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CoffeeMachine.Domain.Entities;
using CoffeeMachine.Domain.Interfaces.Repositories;

namespace CoffeeMachine.Infrastructure.Repositories
{
    public class PaymentRepository : BaseRepository<Payment>
    {
        public PaymentRepository(DataContext db) : base(db)
        {
        }
    }
}
