using System.Collections.Generic;
using System.Linq;

using CoffeeMachine.Application.Strategy.Base;
using CoffeeMachine.Domain.Dto;
using CoffeeMachine.Domain.Entities;

namespace CoffeeMachine.Application.Strategy.Strategies
{
    /// <summary>
    /// strategy that calc deal of big denomination banknotes
    /// </summary>
    public class BigDeal : IDeal
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="cashbox"><inheritdoc/></param>
        /// <param name="amountDeal"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public (List<BanknoteDto>, List<BanknoteCashbox>) CalcBanknotesDeal(List<BanknoteCashbox> cashbox, int amountDeal)
        {
            List<BanknoteDto> deal = new();
            var numberBanknoteDeal = 0;
            cashbox.Sort();
            cashbox.Reverse();
            foreach (var banknote in cashbox.Where(banknote =>
                         banknote.Denomination <= amountDeal && banknote.CountBanknote > 0))
            {
                do
                {
                    amountDeal -= banknote.Denomination;
                    banknote.CountBanknote--;
                    numberBanknoteDeal++;
                }
                while (banknote.CountBanknote > 0 &&
                       banknote.Denomination <= amountDeal);

                deal.Add(new BanknoteDto { Denomination = banknote.Denomination, CountBanknote = numberBanknoteDeal });
                numberBanknoteDeal = 0;
            }

            return amountDeal == 0 ? (deal, cashbox) : (null, null);
        }
    }
}