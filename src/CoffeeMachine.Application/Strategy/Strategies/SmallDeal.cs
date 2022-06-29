using System.Collections.Generic;
using System.Linq;

using CoffeeMachine.Application.Strategy.Base;
using CoffeeMachine.Domain.Dto;
using CoffeeMachine.Domain.Entities;

namespace CoffeeMachine.Application.Strategy.Strategies
{
    /// <summary>
    /// strategy that calc deal of small denomination banknotes
    /// </summary>
    public class SmallDeal : BaseStrategyDeal, IDeal
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
            var numberBanknoteDeal = 0;
            cashbox = cashbox.Where(banknote => banknote.Denomination <= amountDeal && banknote.CountBanknote > 0)
                .ToList();
            cashbox.Sort();
            for (var i = 0; i < cashbox.Count; i++)
            {
                var allowableNumberBanknote = amountDeal != cashbox[i].Denomination
                    ? 20 * ((float)cashbox[i].CountBanknote / 100)
                    : 1;
                while (cashbox[i].Denomination <= amountDeal &&
                       numberBanknoteDeal <= allowableNumberBanknote &&
                       cashbox[i].CountBanknote > 0)
                {
                    amountDeal -= cashbox[i].Denomination;
                    cashbox[i].CountBanknote--;
                    numberBanknoteDeal++;
                }

                if (numberBanknoteDeal != 0)
                    deal = AddBanknotesInDeal(cashbox[i].Denomination, deal, numberBanknoteDeal);

                numberBanknoteDeal = 0;

                i = CheckDeal(i, cashbox, amountDeal);

                if (i == -2)
                    break;
            }

            return amountDeal == 0 ? deal : null;
        }
    }
}