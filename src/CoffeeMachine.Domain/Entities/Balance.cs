using System;
using System.ComponentModel.DataAnnotations;

namespace CoffeeMachine.Domain.Entities
{
    /// <summary>
    /// Displays amount of money, earned by coffee machine
    /// </summary>
    public class Balance
    {
        /// <summary>
        /// id 'Balance' table
        /// </summary>
        [Key]
        public Guid BalanceId { get; set; }

        /// <summary>
        /// type of coffee
        /// </summary>
        [Required]
        public Coffee Coffee { get; set; }

        /// <summary>
        /// earned money with coffee
        /// </summary>
        [Required]
        public int EarnedMoney { get; set; }
    }
}