using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using CoffeeMachine.Application.Mappers;
using CoffeeMachine.Application.Service.Interfaces;
using CoffeeMachine.Domain.DTO;
using CoffeeMachine.Infrastructure;

namespace CoffeeMachine.Application.Service
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public class CoffeeService : ICoffeeService
    {
        private readonly UnitOfWork _uow;

        public CoffeeService(UnitOfWork uow)
        {
            _uow = uow;
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns><inheritdoc/></returns>
        public async Task<CoffeeDto> GetCoffeeDtoByIdAsync(Guid id)
        {
            var coffee = await _uow.CoffeeRepo.GetByIdAsync(id);
            if (coffee is null)
                return null;
            return Mapper.MapToCoffeeDto(coffee);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns><inheritdoc/></returns>
        public async Task<List<CoffeeDto>> GetListCoffeeDtoAsync()
        {
            return Mapper.MapToListCoffeeDto(await _uow.CoffeeRepo.GetAllAsync());
        }
    }
}