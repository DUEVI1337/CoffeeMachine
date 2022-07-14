using System.Collections.Generic;

using CoffeeMachine.Application.Dto;
using CoffeeMachine.Application.Strategy.Base;
using CoffeeMachine.Domain.Entities;

using Serilog;

namespace CoffeeMachine.Application.Strategy.Strategies
{
    /// <summary>
    /// strategy that calc deal of evenly denomination banknotes
    /// </summary>
    public class EvenlyDeal : BaseStrategyDeal, IDeal
    {
        ///<inheritdoc/>
        public (List<BanknoteDto>, List<BanknoteCashbox>) GetDeal(List<BanknoteCashbox> cashbox,
            int amountDeal)
        {
            List<BanknoteDto> deal = new();
            var result = (deal, cashbox);
            cashbox.Sort();
            cashbox.Reverse();
            for (var i = 0; i < cashbox.Count; i++)
            {
                if (cashbox[i].Denomination > amountDeal || cashbox[i].CountBanknote == 0)
                {
                    i = cashbox.FindIndex(x => x.Denomination <= amountDeal && x.CountBanknote > 0);
                    if (i == -1)
                        break;
                }

                amountDeal -= cashbox[i].Denomination;
                cashbox[i].CountBanknote--;
                deal = AddBanknoteInDeal(cashbox[i].Denomination, deal, 1);

                if (amountDeal == 0)
                    return result;
            }

            Log.Information($"Strategy {this} fail");
            return (null, null);
        }
    }
}