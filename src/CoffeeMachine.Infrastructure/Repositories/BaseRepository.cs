using CoffeeMachine.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoffeeMachine.Infrastructure.Repositories
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DataContext _db;

        public BaseRepository(DataContext db)
        {
            _db = db;
        }

        public async Task<TEntity> GetByIdAsync(int id) =>
            await _db.Set<TEntity>().FindAsync(id);

        public async Task<List<TEntity>> GetAllAsync() =>
            await _db.Set<TEntity>().ToListAsync();

        public async Task AddAsync(TEntity entity) =>
            await _db.Set<TEntity>().AddAsync(entity);

        public void Update(TEntity entity) =>
           _db.Set<TEntity>().Update(entity);

        public void Delete(TEntity entity) =>
            _db.Set<TEntity>().Remove(entity);
    }
}
