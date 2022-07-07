using System;

namespace CoffeeMachine.Application.Dto
{
    /// <summary>
    /// represent 'Balance' in database
    /// </summary>
    public class BalanceDto
    {
        /// <summary>
        /// id 'Balance'
        /// </summary>
        public Guid BalanceId { get; set; }

        /// <summary>
        /// Id 'Coffee'
        /// </summary>
        public Guid CoffeeId { get; set; }

        /// <summary>
        /// earned money with coffee
        /// </summary>
        public int EarnedMoney { get; set; }
    }
}