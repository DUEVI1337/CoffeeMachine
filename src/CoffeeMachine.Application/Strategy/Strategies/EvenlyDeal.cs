using System.Collections.Generic;
using System.Linq;

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
        public (List<BanknoteDto>, List<BanknoteCashbox>) CalcBanknotesDeal(List<BanknoteCashbox> cashbox,
            int amountDeal)
        {
            List<BanknoteDto> deal = new();
            var result = (deal, cashbox);
            var weight = 0;
            cashbox.Reverse();
            while (amountDeal != 0)
            {
                var i = cashbox.IndexOf(
                    cashbox.FirstOrDefault(x => x.Denomination <= amountDeal && x.CountBanknote > 0));
                if (cashbox.Select(x => x.Denomination).Contains(amountDeal))
                {
                    amountDeal -= cashbox[i].Denomination;
                    cashbox[i].CountBanknote--;
                    deal = AddBanknoteInDeal(cashbox[i].Denomination, deal, 1);
                    break;
                }

                cashbox = cashbox.Where(x => x.CountBanknote > 0 && x.Denomination < amountDeal).ToList();
                var sum = cashbox.Sum(x => x.Denomination);
                if (sum <= amountDeal)
                {
                    if ((amountDeal % 100 == 50 && sum % 100 != 50) ||
                        (amountDeal % 100 != 50 && sum % 100 == 50))
                    {
                        i = cashbox.IndexOf(cashbox.FirstOrDefault(x =>
                            x.Denomination <= amountDeal && x.CountBanknote > 0));
                        if (i == -1)
                            break;

                        amountDeal -= cashbox[i].Denomination;
                        cashbox[i].CountBanknote--;
                        deal = AddBanknoteInDeal(cashbox[i].Denomination, deal, 1);
                    }

                    weight = amountDeal / sum;
                    var minBanknote = cashbox.Select(x => x.CountBanknote).Min();
                    if (minBanknote > weight)
                        minBanknote = weight;

                    sum *= minBanknote;
                    amountDeal -= sum;
                    foreach (var banknote in cashbox)
                    {
                        banknote.CountBanknote -= minBanknote;
                    }

                    cashbox.ForEach(x => deal = AddBanknoteInDeal(x.Denomination, deal, minBanknote));
                }
                else
                {
                    for (var j = 0; j < cashbox.Count; j++)
                    {
                        if (cashbox[j].Denomination <= amountDeal)
                        {
                            amountDeal -= cashbox[j].Denomination;
                            cashbox[j].CountBanknote--;
                            deal = AddBanknoteInDeal(cashbox[j].Denomination, deal, 1);

                            if (amountDeal == 0)
                                return result;
                        }

                        if (j == cashbox.Count - 1)
                        {
                            j = cashbox.IndexOf(cashbox.FirstOrDefault(x =>
                                x.Denomination <= amountDeal && x.CountBanknote > 0)) - 1;
                            if (j == -2)
                                break;
                        }
                    }
                }
            }

            if (amountDeal == 0)
                return result;

            Log.Information($"Strategy {this} fail");
            return (null, null);
        }
    }
}