using System.Threading.Tasks;

using CoffeeMachine.Domain.Entities;

namespace CoffeeMachine.Application.Services.Interfaces
{
    /// <summary>
    /// Work with <see cref="Payment"/> entity from 'Infrastructure' layer
    /// </summary>
    public interface IPaymentService
    {
        /// <summary>
        /// create and add 'Payment' in database
        /// </summary>
        /// <param name="amountClientMoney">money that client contributed to coffee machine</param>
        /// <param name="coffeeId">id of coffee that want buy client</param>
        /// <param name="amountDeal">money that need give client</param>
        void AddPaymentAsync(int amountClientMoney, string coffeeId, int amountDeal);
    }
}