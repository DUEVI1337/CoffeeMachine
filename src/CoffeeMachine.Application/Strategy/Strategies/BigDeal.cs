using System.Collections.Generic;
using System.Linq;

using CoffeeMachine.Application.Dto;
using CoffeeMachine.Application.Strategy.Base;
using CoffeeMachine.Domain.Entities;

using Serilog;


namespace CoffeeMachine.Application.Strategy.Strategies
{
    /// <summary>
    /// strategy that calc deal of big denomination banknotes.
    /// </summary>
    public class BigDeal : IDeal
    {
        ///<inheritdoc/>
        public (List<BanknoteDto>, List<BanknoteCashbox>) GetDeal(List<BanknoteCashbox> cashbox,
            int amountDeal)
        {
            List<BanknoteDto> deal = new();
            var result = (deal, cashbox);
            cashbox.Sort();
            cashbox.Reverse();

            foreach (var banknote in cashbox.Where(banknote =>
                         banknote.Denomination <= amountDeal && banknote.CountBanknote > 0))
            {
                var numberBanknoteDeal = amountDeal / banknote.Denomination;
                if (numberBanknoteDeal <= banknote.CountBanknote)
                {
                    amountDeal -= banknote.Denomination * numberBanknoteDeal;
                    banknote.CountBanknote -= numberBanknoteDeal;
                }
                else
                {
                    numberBanknoteDeal = banknote.CountBanknote;
                    amountDeal -= banknote.Denomination * numberBanknoteDeal;
                    banknote.CountBanknote = 0;
                }

                deal.Add(new BanknoteDto { Denomination = banknote.Denomination, CountBanknote = numberBanknoteDeal });
                if (amountDeal == 0)
                    return result;
            }

            if (amountDeal == 0)
                return result;

            Log.Information($"Strategy {this} fail");
            return (null, null);
        }
    }
}