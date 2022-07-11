
using System.Threading.Tasks;

using CoffeeMachine.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace CoffeeMachine.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(DataContext db) : base(db)
        {
        }
    }
}
