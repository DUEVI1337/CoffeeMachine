using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CoffeeMachine.Application.Dto;
using CoffeeMachine.Application.Strategy.Base;
using CoffeeMachine.Domain.Entities;

using Serilog;

namespace CoffeeMachine.Application.Strategy.Strategies
{
    public class DynamicDeal : BaseStrategyDeal, IDeal
    {
        public (List<BanknoteDto>, List<BanknoteCashbox>) GetDeal(List<BanknoteCashbox> cashbox,
            int amountDeal)
        {
            List<BanknoteDto> deal = new();
            var result = (deal, cashbox);
            cashbox.Sort();
            cashbox.Reverse();
            DynamicCalcBanknotesToDeal(amountDeal, cashbox, ref deal);
            if(amountDeal == 0)
                return result;

            Log.Information($"Strategy {this} fail");
            return (null, null);
        }

        private void DynamicCalcBanknotesToDeal(int amountDeal, List<BanknoteCashbox> cashbox, ref List<BanknoteDto> deal)
        {
            if (cashbox.Where(x => x.CountBanknote > 0).Select(x => x.Denomination).Contains(amountDeal))
            {
                var banknote = cashbox.FirstOrDefault(x => x.Denomination == amountDeal);
                cashbox.FirstOrDefault(x => x.Denomination == amountDeal)!.CountBanknote--;
                deal = AddBanknoteInDeal(banknote.Denomination, deal, 1);
                return;
            }

            foreach (var banknote in cashbox.Where(x => x.Denomination <= amountDeal && x.CountBanknote > 0))
            {
                DynamicCalcBanknotesToDeal(amountDeal - banknote.Denomination, cashbox, ref deal);
                if (deal.Count == 0)
                    continue;

                if (banknote.CountBanknote <= 0) //banknotes ran out on recursion 
                {
                    deal.Clear();
                    break;
                }

                deal = AddBanknoteInDeal(banknote.Denomination, deal, 1);
                banknote.CountBanknote--;

                return;
            }
        }
    }
}
