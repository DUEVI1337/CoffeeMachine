using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CoffeeMachine.Domain.Entities;

namespace CoffeeMachine.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(DataContext db) : base(db)
        {
        }
    }
}
