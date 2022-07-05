using System;
using System.Collections.Generic;
using System.Linq;

using CoffeeMachine.Application.Strategy.Base;
using CoffeeMachine.Domain.Dto;
using CoffeeMachine.Domain.Entities;

using Serilog;

namespace CoffeeMachine.Application.Strategy.Strategies
{
    /// <summary>
    /// strategy that calc deal of small denomination banknotes
    /// </summary>
    public class SmallDeal : BaseStrategyDeal, IDeal
    {
        private readonly Func<List<BanknoteCashbox>, int, List<BanknoteCashbox>> _sortList = (cashbox, deal) =>
        {
            cashbox.Sort();
            return cashbox.Where(x => x.Denomination >= deal && x.CountBanknote > 0).ToList();
        };

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="cashbox"><inheritdoc/></param>
        /// <param name="amountDeal"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public (List<BanknoteDto>, List<BanknoteCashbox>) CalcBanknotesDeal(List<BanknoteCashbox> cashbox,
            int amountDeal)
        {
            List<BanknoteDto> deal = new();
            cashbox = _sortList(cashbox, amountDeal);
            var numberBanknoteDeal = 0;
            for (var i = 0; i < cashbox.Count; i++)
            {
                var allowableNumberBanknote =
                    amountDeal !=
                    cashbox[i].Denomination //allowable number of banknotes to deal from cashbox of coffee machine (20% or one banknote)
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
                    deal = AddBanknoteInDeal(cashbox[i].Denomination, deal, numberBanknoteDeal);
                numberBanknoteDeal = 0;
                i = CheckDeal(i, cashbox, amountDeal) - 1; //next iteration of loop - i++
                if (i == -2)
                    break;
            }

            if (amountDeal == 0)
                return (deal, cashbox);

            Log.Information($"Strategy {this} fail");
            return (null, null);
        }
    }
}