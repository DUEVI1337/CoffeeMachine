using System.Collections.Generic;
using System.Linq;

using CoffeeMachine.Application.Dto;

namespace CoffeeMachine.Application.Strategy.Base
{
    /// <summary>
    /// Contain methods that are used by strategies for calc deal
    /// </summary>
    public abstract class BaseStrategyDeal
    {
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
            {
                dealBanknotes.Add(new BanknoteDto
                {
                    Denomination = banknoteDenomination,
                    CountBanknote = numberBanknote
                });
            }
            else
            {
                dealBanknotes.FirstOrDefault(x => x.Denomination == banknoteDenomination)!.CountBanknote +=
                    numberBanknote;
            }

            return dealBanknotes;
        }
    }
}