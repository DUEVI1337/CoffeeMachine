using System;
using System.ComponentModel.DataAnnotations;

namespace CoffeeMachine.Domain.Entities
{
    /// <summary>
    /// Total income coffee machine from all types coffee
    /// </summary>
    public class Income
    {
        /// <summary>
        /// Date
        /// </summary>
        [Required]
        public DateTime Date { get; set; }

        /// <summary>
        /// id 'Income' table
        /// </summary>
        [Key]
        public Guid IncomeId { get; set; }

        /// <summary>
        /// Total income coffee machine
        /// </summary>
        [Required]
        public int TotalIncome { get; set; }
    }
}