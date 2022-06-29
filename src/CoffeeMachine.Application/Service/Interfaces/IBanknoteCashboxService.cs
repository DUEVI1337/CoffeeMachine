using System.Collections.Generic;
using System.Threading.Tasks;

using CoffeeMachine.Domain.Entities;

namespace CoffeeMachine.Application.Service.Interfaces
{
    /// <summary>
    /// Work with <see cref="BanknoteCashBox"/> entity from 'Infrastructure' layer
    /// </summary>
    public interface IBanknoteCashboxService
    {
        /// <summary>
        /// get money in cashbox of coffee machine 
        /// </summary>
        /// <returns><see cref="List{T}"/> where T <see cref="BanknoteCashBox"/></returns>
        Task<List<BanknoteCashBox>> GetCashboxAsync();
    }
}