using System.Collections.Generic;
using System.Linq;

using CoffeeMachine.Application.Dto;
using CoffeeMachine.Application.Strategy.Base;
using CoffeeMachine.Domain.Entities;

using Serilog;

namespace CoffeeMachine.Application.Strategy.Strategies
{
    /// <summary>
    /// strategy that calc deal of small denomination banknotes
    /// </summary>
    public class SmallDeal : BaseStrategyDeal, IDeal
    {
        ///<inheritdoc/>
        public (List<BanknoteDto>, List<BanknoteCashbox>) GetDeal(List<BanknoteCashbox> cashbox,
            int amountDeal)
        {
            List<BanknoteDto> deal = new();
            var result = (deal, cashbox);
            cashbox.Sort();
            for (var i = 0; i < cashbox.Count; i++)
            {
                if (cashbox[i].Denomination > amountDeal || cashbox[i].CountBanknote == 0)
                {
                    i = cashbox.FindIndex(x => x.Denomination <= amountDeal && x.CountBanknote > 0);
                    if (i == -1)
                        break;
                }
                //allowable number of banknotes to give from cashbox of coffee machine (10% or one banknote)
                var allowableNumberBanknote = amountDeal > cashbox[i].Denomination
                    ? (float)(0.1 * cashbox[i].CountBanknote)
                    : 1;

                allowableNumberBanknote = allowableNumberBanknote == 0 ? 1 : allowableNumberBanknote;

                //number of banknotes given denomination to pay off amount of deal
                var numberBanknoteDeal = amountDeal / cashbox[i].Denomination;

                if (numberBanknoteDeal > allowableNumberBanknote)
                    numberBanknoteDeal = (int)allowableNumberBanknote;

                amountDeal -= cashbox[i].Denomination * numberBanknoteDeal;
                cashbox[i].CountBanknote -= numberBanknoteDeal;
                deal = AddBanknoteInDeal(cashbox[i].Denomination, deal, numberBanknoteDeal);

                if (amountDeal == 0)
                    return result;

                if (i == cashbox.Count - 1)
                    i -= 1;
            }

            Log.Information($"Strategy '{this}' fail");
            return (null, null);
        }
    }
}