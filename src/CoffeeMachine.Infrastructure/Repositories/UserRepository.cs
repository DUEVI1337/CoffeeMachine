using CoffeeMachine.Domain.Entities;

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