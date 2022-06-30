using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeMachine.Domain.Entities
{
    /// <summary>
    /// payment for selected coffee
    /// </summary>
    public class Payment
    {
        /// <summary>
        /// money that person contributed in coffee machine
        /// </summary>
        [Required]
        public int ClientMoney { get; set; }

        /// <summary>
        /// Navigation property to 'CoffeeId'
        /// </summary>
        public Coffee Coffee { get; set; }

        /// <summary>
        /// type of coffee
        /// </summary>
        [Required]
        [ForeignKey(nameof(Coffee))]
        public Guid CoffeeId { get; set; }

        /// <summary>
        /// money that coffee machine returned person 
        /// </summary>
        [Required]
        public int Deal { get; set; }

        /// <summary>
        /// id in database table
        /// </summary>
        [Key]
        public Guid PaymentId { get; set; }
    }
}