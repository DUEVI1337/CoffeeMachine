using System.Collections.Generic;

using CoffeeMachine.Application.Strategy.Base;
using CoffeeMachine.Application.Strategy.Strategies;

namespace CoffeeMachine.Application.Strategy
{
    /// <summary>
    /// Select strategy for calc deal
    /// </summary>
    public class DealFactory
    {
        /// <summary>
        /// Contain available strategy (name strategy, instance strategy)
        /// </summary>
        private static readonly Dictionary<string, IDeal> _strategiesDeal = new()
        {
            { nameof(BigDeal), new BigDeal() },
            { nameof(SmallDeal), new SmallDeal() },
            { nameof(EvenlyDeal), new EvenlyDeal() }
        };

        /// <summary>
        /// Find strategy by name
        /// </summary>
        /// <param name="nameStrategy"></param>
        /// <returns><see cref="IDeal"/> strategy</returns>
        public static IDeal GetDealStrategy(string nameStrategy)
        {
            return _strategiesDeal[nameStrategy];
        }
    }
}