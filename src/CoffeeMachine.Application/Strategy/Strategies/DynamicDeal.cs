using System.Collections.Generic;
using System.Linq;

using CoffeeMachine.Application.Dto;
using CoffeeMachine.Application.Strategy.Base;
using CoffeeMachine.Domain.Entities;

using Serilog;

namespace CoffeeMachine.Application.Strategy.Strategies
{
    /// <summary>
    /// strategy that calc banknotes to deal checking all possible options. Based on theory of "Dynamic Programming"
    /// </summary>
    public class DynamicDeal : BaseStrategyDeal, IDeal
    {
        ///<inheritdoc/>
        public (List<BanknoteDto>, List<BanknoteCashbox>) GetDeal(List<BanknoteCashbox> cashbox,
            int amountDeal)
        {
            List<BanknoteDto> deal = new();
            var result = (deal, cashbox);
            cashbox.Sort();
            cashbox.Reverse();

            DynamicCalcBanknotes(amountDeal, cashbox, ref deal);

            if (deal.Count != 0)
                return result;

            Log.Information($"Strategy {this} fail");
            return (null, null);
        }

        /// <summary>
        /// Implementation of "Dynamic Programming"
        /// </summary>
        /// <param name="amountDeal">amount of deal</param>
        /// <param name="cashbox">cashbox of coffee machine</param>
        /// <param name="deal">amount of money to give client</param>
        private static void DynamicCalcBanknotes(int amountDeal, List<BanknoteCashbox> cashbox,
            ref List<BanknoteDto> deal)
        {
            var oneBanknote = cashbox.FirstOrDefault(x => x.Denomination == amountDeal && x.CountBanknote > 0);
            if (oneBanknote != null)
            {
                oneBanknote.CountBanknote--;
                deal = AddBanknoteInDeal(oneBanknote.Denomination, deal, 1);
                return;
            }
            foreach (var banknote in cashbox.Where(x => x.Denomination <= amountDeal && x.CountBanknote > 0))
            {
                DynamicCalcBanknotes(amountDeal - banknote.Denomination, cashbox, ref deal);

                //banknotes ran out on recursion 
                if (deal.Count == 0 || banknote.CountBanknote <= 0)
                {
                    deal.Clear();
                    continue;
                }

                banknote.CountBanknote--;
                deal = AddBanknoteInDeal(banknote.Denomination, deal, 1);
            }
        }
    }
}