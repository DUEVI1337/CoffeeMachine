using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoffeeMachine.Domain.Interfaces.Repositories
{
    /// <summary>
    /// interface for generic repository
    /// </summary>
    /// <typeparam name="TEntity">entity from database</typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// add entity in database
        /// </summary>
        /// <param name="entity"></param>
        void Add(TEntity entity);

        /// <summary>
        /// delete entity from database
        /// </summary>
        /// <param name="entity"></param>
        void Delete(TEntity entity);

        /// <summary>
        /// get all entity from database
        /// </summary>
        /// <returns><see cref="List{T}"/></returns>
        Task<List<TEntity>> GetAllAsync();

        /// <summary>
        /// get entity by him id from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns><see cref="TEntity"/></returns>
        Task<TEntity> GetByIdAsync(Guid id);

        /// <summary>
        /// update info about entity by him id in database
        /// </summary>
        /// <param name="entity"></param>
        void Update(TEntity entity);
    }
}