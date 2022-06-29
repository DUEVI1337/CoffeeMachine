using System.Collections.Generic;

using CoffeeMachine.Application.Strategy.Base;
using CoffeeMachine.Domain.Dto;
using CoffeeMachine.Domain.Entities;

namespace CoffeeMachine.Application.Strategy.Contexts
{
    /// <summary>
    /// using strategies that calc deal
    /// </summary>
    public class DealContext
    {
        private readonly IDeal _deal;

        public DealContext(IDeal deal)
        {
            _deal = deal;
        }

        /// <summary>
        /// using algorithm strategy
        /// </summary>
        /// <param name="cashbox">cashbox of coffee machine</param>
        /// <param name="amountDeal">amount money that need give to client</param>
        /// <returns><see cref="List{T}"/> where T <see cref="BanknoteDto"/></returns>
        public List<BanknoteDto> GiveDeal(List<BanknoteCashBox> cashbox, int amountDeal)
        {
            return _deal.CalcBanknotesDeal(cashbox, amountDeal);
        }
    }
}