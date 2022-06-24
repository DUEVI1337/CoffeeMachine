using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using CoffeeMachine.Domain.Interfaces.Repositories;

using Microsoft.EntityFrameworkCore;

namespace CoffeeMachine.Infrastructure.Repositories
{
    /// <summary>
    /// base class for all repository
    /// </summary>
    /// <typeparam name="TEntity">entity from database</typeparam>
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DataContext _db;

        public BaseRepository(DataContext db)
        {
            _db = db;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="entity"></param>
        public void Add(TEntity entity)
        {
            _db.Set<TEntity>().AddAsync(entity);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(TEntity entity)
        {
            _db.Set<TEntity>().Remove(entity);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns><inheritdoc/></returns>
        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _db.Set<TEntity>().ToListAsync();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns><inheritdoc/></returns>
        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await _db.Set<TEntity>().FindAsync(id);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="entity"></param>
        public void Update(TEntity entity)
        {
            _db.Set<TEntity>().Update(entity);
        }
    }
}