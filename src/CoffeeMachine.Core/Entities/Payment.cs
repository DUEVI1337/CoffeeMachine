using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeMachine.Core.Entities
{
    public class Payment
    {
        /// <summary>
        /// money that coffee machine returned person 
        /// </summary>
        [Required]
        public int CashDepositAmount { get; set; }

        /// <summary>
        /// money that person contributed in coffee machine
        /// </summary>
        [Required]
        public int ContributedMoney { get; set; }

        /// <summary>
        /// id coffee that was buy
        /// </summary>
        public Coffee Coffee { get; set; }

        /// <summary>
        /// id in database table
        /// </summary>
        [Key]
        public Guid PaymentId { get; set; }
    }
}