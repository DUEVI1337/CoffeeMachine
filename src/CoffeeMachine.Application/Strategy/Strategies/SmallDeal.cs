using System;
using System.Collections.Generic;
using System.Linq;

using CoffeeMachine.Application.Strategy.Base;
using CoffeeMachine.Application.Dto;
using CoffeeMachine.Domain.Entities;

using Serilog;

namespace CoffeeMachine.Application.Strategy.Strategies
{
    /// <summary>
    /// strategy that calc deal of small denomination banknotes
    /// </summary>
    public class SmallDeal : BaseStrategyDeal, IDeal
    {
        public (List<BanknoteDto>, List<BanknoteCashbox>) CalcBanknotesDeal(List<BanknoteCashbox> cashbox,
            int amountDeal)
        {
            List<BanknoteDto> deal = new();
            for (var i = 0; i < cashbox.Count; i++)
            {
                if (cashbox[i].Denomination > amountDeal)
                {
                    i = cashbox.IndexOf(cashbox.FirstOrDefault(x => x.Denomination <= amountDeal && x.CountBanknote > 0));
                    if (i == -1)
                        break;
                }

                var allowableNumberBanknote =
                    amountDeal > cashbox[i].Denomination //allowable number of banknotes to deal from cashbox of coffee machine (20% or one banknote)
                        ? (float)(0.2 * cashbox[i].CountBanknote)
                        : 1;

                allowableNumberBanknote = allowableNumberBanknote == 0 ? 1 : allowableNumberBanknote;

                var numberBanknoteDeal = amountDeal / cashbox[i].Denomination;
                if (numberBanknoteDeal <= allowableNumberBanknote)
                {
                    amountDeal -= cashbox[i].Denomination * numberBanknoteDeal;
                    cashbox[i].CountBanknote -= numberBanknoteDeal;
                }
                else
                {
                    numberBanknoteDeal = (int)allowableNumberBanknote;
                    amountDeal -= cashbox[i].Denomination * numberBanknoteDeal;
                    cashbox[i].CountBanknote -= numberBanknoteDeal;
                }

                deal = AddBanknoteInDeal(cashbox[i].Denomination, deal, numberBanknoteDeal);
            }

            if (amountDeal == 0)
                return (deal, cashbox);

            Log.Information($"Strategy {this} fail");
            return (null, null);
        }
    }
}