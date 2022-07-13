using System.Collections.Generic;
using System.Threading.Tasks;

using CoffeeMachine.Domain.Entities;

namespace CoffeeMachine.Application.Services.Interfaces
{
    /// <summary>
    /// Work with <see cref="BanknoteCashbox"/> entity from 'Infrastructure' layer
    /// </summary>
    public interface IBanknoteCashboxService
    {
        /// <summary>
        /// get money in cashbox of coffee machine 
        /// </summary>
        /// <returns><see cref="List{T}"/> where T <see cref="BanknoteCashbox"/></returns>
        Task<List<BanknoteCashbox>> GetCashboxAsync();

        /// <summary>
        /// update cashbox of coffee machine in database
        /// </summary>
        /// <param name="updatedCashbox">cashbox with new data</param>
        Task UpdateCashboxAsync(List<BanknoteCashbox> updatedCashbox);

        /// <summary>
        /// Copying <see cref="List{T}"/> to new <see cref="List{T}"/> for keep to original list
        /// </summary>
        /// <param name="cashbox">cashbox of coffee machine</param>
        /// <returns><see cref="List{T}"/> where T <see cref="BanknoteCashbox"/></returns>
        List<BanknoteCashbox> GetCopyCashbox(List<BanknoteCashbox> cashbox, int amountDeal);
    }
}