using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoffeeMachine.Domain.Entities
{
    public class Coffee
    {
        /// <summary>
        /// id in database table
        /// </summary>
        [Key]
        public Guid CoffeeId { get; set; }

        /// <summary>
        /// brand of coffee
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// navigation property for relationship (Payment list for this coffee)
        /// </summary>
        public ICollection<Payment> Payments { get; set; }

        /// <summary>
        /// price of coffee
        /// </summary>
        [Required]
        public int Price { get; set; }
    }
}