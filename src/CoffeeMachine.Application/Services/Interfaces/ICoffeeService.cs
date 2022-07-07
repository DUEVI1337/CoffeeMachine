using System.Collections.Generic;
using System.Threading.Tasks;

using CoffeeMachine.Application.Dto;
using CoffeeMachine.Domain.Entities;

namespace CoffeeMachine.Application.Services.Interfaces
{
    /// <summary>
    /// Work with <see cref="Coffee"/> entity from 'Infrastructure' layer. For CoffeeController.
    /// </summary>
    public interface ICoffeeService
    {
        /// <summary>
        /// Checking price of coffee and client money, calculate deal and if necessary change type deal
        /// </summary>
        /// <param name="coffee">coffee from order</param>
        /// <param name="clientMoney">money that client was injected into the coffee machine</param>
        /// <param name="typeDeal">type of deal that client chose</param>
        /// <returns><see cref="List{T}"/> where T <see cref="BanknoteDto"/></returns>
        Task<List<BanknoteDto>> BuyCoffeeAsync(CoffeeDto coffee, List<BanknoteDto> clientMoney, TypeDeal typeDeal);

        /// <summary>
        /// get <see cref="Coffee"/> by him id from database that converts to <see cref="CoffeeDto"/>
        /// </summary>
        /// <param name="id">id coffee</param>
        /// <returns><see cref="CoffeeDto"/> or null</returns>
        Task<CoffeeDto> GetCoffeeDtoByIdAsync(string id);

        /// <summary>
        /// get all <see cref="Coffee"/> in database that converts to list <see cref="CoffeeDto"/>
        /// </summary>
        /// <returns><see cref="List{T}"/> where T <see cref="CoffeeDto"/></returns>
        Task<List<CoffeeDto>> GetListCoffeeDtoAsync();
    }
}