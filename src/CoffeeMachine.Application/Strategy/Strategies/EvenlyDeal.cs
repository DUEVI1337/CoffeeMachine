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
    /// strategy that calc deal of evenly denomination banknotes
    /// </summary>
    public class EvenlyDeal : BaseStrategyDeal, IDeal
    {
        public (List<BanknoteDto>, List<BanknoteCashbox>) CalcBanknotesDeal(List<BanknoteCashbox> cashbox,
            int amountDeal)
        {
            List<BanknoteDto> deal = new();
            cashbox.Reverse();
            for (var i = 0; i < cashbox.Count; i++)
            {
                amountDeal -= cashbox[i].Denomination;
                cashbox[i].CountBanknote--;
                deal = AddBanknoteInDeal(cashbox[i].Denomination, deal, 1);
                i = GetBanknoteIndex(i, cashbox, amountDeal) - 1; //'-1' - next iteration of loop i + 1
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