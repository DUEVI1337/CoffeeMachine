using System;
using System.ComponentModel.DataAnnotations;

namespace CoffeeMachine.Domain.Entities
{
    /// <summary>
    /// banknote in cashbox coffee machine
    /// </summary>
    public class BanknoteCashBox : IComparable<BanknoteCashBox>
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

        /// <summary>
        /// Specify property by which will sort List
        /// </summary>
        /// <param name="banknote"></param>
        /// <returns></returns>
        public int CompareTo(BanknoteCashBox banknote)
        {
            return Denomination.CompareTo(banknote.Denomination);
        }
    }
}