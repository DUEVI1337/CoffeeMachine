using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoffeeMachine.Application.Dto
{
    /// <summary>
    /// type deal that cas chose client
    /// </summary>
    public enum TypeDeal
    {
        /// <summary>
        /// deal of big denomination banknotes
        /// </summary>
        BigDeal = 1,

        /// <summary>
        /// deal of small denomination banknotes
        /// </summary>
        SmallDeal = 2,

        /// <summary>
        /// deal of even denomination banknotes
        /// </summary>
        EvenlyDeal = 3
    }


    /// <summary>
    /// Banknote
    /// </summary>
    public class OrderDto
    {
        /// <summary>
        /// money that person contributed in coffee machine
        /// </summary>
        public List<BanknoteDto> Banknotes { get; set; }

        /// <summary>
        /// coffee that person want to buy 
        /// </summary>
        public string CoffeeId { get; set; }

        /// <summary>
        /// type change money person.
        /// Valid values: 1-Big change money, 2-small change money, 3-evenly change money
        /// </summary>
        [EnumDataType(typeof(TypeDeal), ErrorMessage = "Invalid type deal")]
        public int TypeDeal { get; set; }
    }
}