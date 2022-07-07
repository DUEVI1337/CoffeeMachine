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
    /// strategy that calc deal of big denomination banknotes
    /// </summary>
    public class BigDeal : IDeal
    {
        private readonly Action<List<BanknoteCashbox>> _sortList = cashbox =>
        {
            cashbox.Sort();
            cashbox.Reverse();
        };

        public (List<BanknoteDto>, List<BanknoteCashbox>) CalcBanknotesDeal(List<BanknoteCashbox> cashbox,
            int amountDeal)
        {
            List<BanknoteDto> deal = new();
            var numberBanknoteDeal = 0;
            _sortList(cashbox);
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

            if (amountDeal == 0)
                return (deal, cashbox);

            Log.Information($"Strategy {this} fail");
            return (null, null);
        }
    }
}