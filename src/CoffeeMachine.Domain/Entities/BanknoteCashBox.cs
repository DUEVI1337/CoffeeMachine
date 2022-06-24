using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeMachine.Domain.Entities
{
    /// <summary>
    /// banknote in cashbox coffee machine
    /// </summary>
    public class BanknoteCashBox
    {
        /// <summary>
        /// id in table database
        /// </summary>
        [Key]
        public Guid BanknoteId { get; set; }

        /// <summary>
        /// count banknote in coffee machine
        /// </summary>
        [Required]
        public int CountBanknote { get; set; }

        /// <summary>
        /// denomination banknote (100руб, 200руб)
        /// </summary>
        [Required]
        public int Denomination { get; set; }
    }
}