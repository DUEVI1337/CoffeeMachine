
using System.Threading.Tasks;

using CoffeeMachine.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace CoffeeMachine.Infrastructure.Repositories
{
    /// <summary>
    /// repository for work with entities <see cref="User"/>
    /// </summary>
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(DataContext db) : base(db)
        {
        }
    }
}
