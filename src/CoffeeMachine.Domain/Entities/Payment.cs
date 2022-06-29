using System;
using System.ComponentModel.DataAnnotations;

namespace CoffeeMachine.Domain.Entities
{
    /// <summary>
    /// payment for selected coffee
    /// </summary>
    public class Payment
    {
        /// <summary>
        /// id coffee that was buy
        /// </summary>
        public Coffee Coffee { get; set; }

        /// <summary>
        /// money that person contributed in coffee machine
        /// </summary>
        [Required]
        public int ClientMoney { get; set; }

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