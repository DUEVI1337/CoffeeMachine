using System.Collections.Generic;

using CoffeeMachine.Application.Dto;
using CoffeeMachine.Domain.Entities;

namespace CoffeeMachine.Application.Strategy.Base
{
    /// <summary>
    /// For strategies that select banknotes for deal of client
    /// </summary>
    public interface IDeal
    {
        /// <summary>
        /// Selecting banknotes from cashbox of coffee machine for deal to client
        /// </summary>
        /// <param name="cashbox">cashbox of coffee machine</param>
        /// <param name="amountDeal">amount money that need give to client</param>
        /// <returns><see cref="List{T}"/> where T <see cref="BanknoteDto"/> and <see cref="List{T}"/> where T <see cref="BanknoteCashbox"/>, or (null,null)</returns>
        (List<BanknoteDto>, List<BanknoteCashbox>) CalcBanknotesDeal(List<BanknoteCashbox> cashbox, int amountDeal);
    }
}