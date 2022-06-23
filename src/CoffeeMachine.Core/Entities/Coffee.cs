using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeMachine.Core.Entities
{
    public class Coffee
    {
        /// <summary>
        /// id in database table
        /// </summary>
        [Key]
        public Guid CoffeeId { get; set; }

        /// <summary>
        /// name coffee
        /// </summary>
        [Required]
        public string Name { get; set; }

        public ICollection<Payment> Payments { get; set; }

        /// <summary>
        /// price coffee
        /// </summary>
        [Required]
        public int Price { get; set; }
    }
}