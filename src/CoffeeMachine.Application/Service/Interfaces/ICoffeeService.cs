using System.Collections.Generic;
using System.Threading.Tasks;

using CoffeeMachine.Domain.Dto;
using CoffeeMachine.Domain.DTO;
using CoffeeMachine.Domain.Entities;

namespace CoffeeMachine.Application.Service.Interfaces
{
    /// <summary>
    /// Work with <see cref="Coffee"/> entity from 'Infrastructure' layer. For CoffeeController.
    /// </summary>
    public interface ICoffeeService
    {
        /// <summary>
        /// Checking price of coffee and client money, calculate deal and if necessary change type deal
        /// </summary>
        /// <param name="coffeePrice">price of coffee</param>
        /// <param name="clientMoney">money that client was injected into the coffee machine</param>
        /// <param name="cashbox">cashbox of coffee machine</param>
        /// <param name="typeDeal">type of deal that client chose</param>
        /// <returns><see cref="List{T}"/> where T <see cref="BanknoteDto"/></returns>
        List<BanknoteDto> BuyCoffee(int coffeePrice, int clientMoney, List<BanknoteCashBox> cashbox, TypeDeal typeDeal);

        /// <summary>
        /// Calculating amount money that client was injected into the coffee machine
        /// </summary>
        /// <param name="banknotes">banknote of client</param>
        /// <returns><see cref="int"/>, amount money of client</returns>
        int GetAmountClientMoney(List<BanknoteDto> banknotes);

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