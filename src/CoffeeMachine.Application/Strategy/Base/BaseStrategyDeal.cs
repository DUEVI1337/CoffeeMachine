using System.Collections.Generic;
using System.Linq;

using CoffeeMachine.Application.Dto;
using CoffeeMachine.Domain.Entities;

namespace CoffeeMachine.Application.Strategy.Base
{
    /// <summary>
    /// Contain methods that are used by strategies for calc deal
    /// </summary>
    public abstract class BaseStrategyDeal
    {
        /// <summary>
        /// index that stoops loop
        /// </summary>
        private const int BREAK_INDEX_LOOP = -1;

        /// <summary>
        /// Add banknotes in deal
        /// </summary>
        /// <param name="banknoteDenomination"> denomination of banknote that need add in deal</param>
        /// <param name="dealBanknotes">list that in need add banknotes</param>
        /// <param name="numberBanknote">number of banknotes</param>
        /// <returns><see cref="List{T}"/> where T <seealso cref="BanknoteDto"/></returns>
        protected static List<BanknoteDto> AddBanknoteInDeal(int banknoteDenomination, List<BanknoteDto> dealBanknotes,
            int numberBanknote)
        {
            if (!dealBanknotes.Select(x => x.Denomination).Contains(banknoteDenomination))
                dealBanknotes.Add(new BanknoteDto
                    { Denomination = banknoteDenomination, CountBanknote = numberBanknote });
            else
                dealBanknotes.FirstOrDefault(x => x.Denomination == banknoteDenomination)!.CountBanknote +=
                    numberBanknote;

            return dealBanknotes;
        }

        /// <summary>
        /// Check amount of deal, find need index of banknote in cashbox of coffee machine or return 'BREAK_INDEX_LOOP'
        /// </summary>
        /// <param name="index">index of loop</param>
        /// <param name="cashbox">cashbox of coffee machine</param>
        /// <param name="amountDeal">amount of deal</param>
        /// <returns><see cref="int"/>, new value of iterator loop</returns>
        protected static int GetBanknoteIndex(int index, List<BanknoteCashbox> cashbox, int amountDeal)
        {
            if ((cashbox[index].Denomination > amountDeal || index == cashbox.Count - 1) && amountDeal > 0)
                return cashbox.IndexOf(cashbox.FirstOrDefault(x =>
                    x.Denomination <= amountDeal && x.CountBanknote != 0));

            return amountDeal == 0 ? BREAK_INDEX_LOOP : index;
        }
    }
}