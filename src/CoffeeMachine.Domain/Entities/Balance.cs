using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [ForeignKey(nameof(Coffee))]
        public Guid CoffeeId { get; set; }

        /// <summary>
        /// Navigation property to 'CoffeeId'
        /// </summary>
        public Coffee Coffee { get; set; }

        /// <summary>
        /// earned money with coffee
        /// </summary>
        [Required]
        public int EarnedMoney { get; set; }
    }
}