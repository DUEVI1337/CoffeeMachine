using System.Threading.Tasks;

using CoffeeMachine.Domain.Entities;

namespace CoffeeMachine.Application.Service.Interfaces
{
    /// <summary>
    /// Work with <see cref="Income"/> entity from 'Infrastructure' layer
    /// </summary>
    public interface IIncomeService
    {
        /// <summary>
        /// create and add 'Payment' in database
        /// </summary>
        /// <param name="coffeePrice">price of coffee that buying client</param>
        Task AddIncomeAsync(int coffeePrice);
    }
}