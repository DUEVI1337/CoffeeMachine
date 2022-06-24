using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using CoffeeMachine.Domain.DTO;
using CoffeeMachine.Domain.Entities;

namespace CoffeeMachine.Application.Service.Interfaces
{
    /// <summary>
    /// Work with <see cref="Coffee"/> entity from infrastructure layer
    /// </summary>
    public interface ICoffeeService
    {
        /// <summary>
        /// get <see cref="Coffee"/> by him id from database that converts to <see cref="CoffeeDto"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns><see cref="CoffeeDto"/> or null</returns>
        Task<CoffeeDto> GetCoffeeDtoByIdAsync(Guid id);
        /// <summary>
        /// get all <see cref="Coffee"/> in database that converts to list <see cref="CoffeeDto"/>
        /// </summary>
        /// <returns><see cref="List{T}"/> where T <see cref="CoffeeDto"/></returns>
        Task<List<CoffeeDto>> GetListCoffeeDtoAsync();
    }
}