using System.Collections.Generic;
using System.Linq;

using CoffeeMachine.Application.Strategy.Base;
using CoffeeMachine.Domain.Dto;
using CoffeeMachine.Domain.Entities;

namespace CoffeeMachine.Application.Strategy.Strategies
{
    /// <summary>
    /// strategy that calc deal of evenly denomination banknotes
    /// </summary>
    public class EvenlyDeal : BaseStrategyDeal, IDeal
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="cashbox"><inheritdoc/></param>
        /// <param name="amountDeal"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public List<BanknoteDto> CalcBanknotesDeal(List<BanknoteCashBox> cashbox, int amountDeal)
        {
            List<BanknoteDto> deal = new();
            cashbox = cashbox.Where(banknote => banknote.Denomination <= amountDeal && banknote.CountBanknote > 0)
                .ToList();
            cashbox.Sort();
            cashbox.Reverse();
            for (var i = 0; i < cashbox.Count; i++)
            {
                amountDeal -= cashbox[i].Denomination;
                cashbox[i].CountBanknote--;

                deal = AddBanknotesInDeal(cashbox[i].Denomination, deal, 1);

                i = CheckDeal(i, cashbox, amountDeal);

                if (i == -2)
                    break;
            }

            return amountDeal == 0 ? deal : null;
        }
    }
}