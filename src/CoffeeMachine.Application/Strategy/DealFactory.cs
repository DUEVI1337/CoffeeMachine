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
        /// Contain strategies that was already used
        /// </summary>
        private static readonly List<string> _nameOldStrategies = new();

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
        /// Contain strategies (from dictionary), this list will be using to move between strategies
        /// </summary>
        private static readonly LinkedList<KeyValuePair<string, IDeal>> _strategiesDealLink = new(_strategiesDeal);

        /// <summary>
        /// Find strategy by name
        /// </summary>
        /// <param name="nameStrategy"></param>
        /// <returns><see cref="IDeal"/> strategy</returns>
        public static IDeal GetDealStrategy(string nameStrategy)
        {
            return _strategiesDeal[nameStrategy];
        }

        /// <summary>
        /// Iterating available strategies
        /// </summary>
        /// <param name="nameStrategy">name of strategy that was last used </param>
        /// <returns><see cref="KeyValuePair"/> - new strategy, key - name new strategy, value - instance new strategy</returns>
        public static KeyValuePair<string, IDeal>? GetNextDealStrategy(string nameStrategy)
        {
            if (_nameOldStrategies.Contains(nameStrategy))
            {
                _nameOldStrategies.Clear();
                return null;
            }

            var newStrategy = _strategiesDealLink.First;
            while (_nameOldStrategies.Contains(newStrategy.Value.Key))
                newStrategy = newStrategy?.Next;
            _nameOldStrategies.Add(newStrategy.Value.Key);
            return newStrategy.Value;
        }
    }
}