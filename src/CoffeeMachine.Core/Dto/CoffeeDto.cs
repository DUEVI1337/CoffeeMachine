using CoffeeMachine.Core.Entities;

namespace CoffeeMachine.Core.DTO
{
    public class CoffeeDto
    {
        /// <summary>
        /// Id entity <see cref="Coffee"/>
        /// </summary>
        public string CoffeeId { get; set; }
        /// <summary>
        /// Name entity <seealso cref="Coffee"/>
        /// </summary>
        public string CoffeeName { get; set; }
        /// <summary>
        /// Price entity <seealso cref="Coffee"/>
        /// </summary>
        public int CoffeePrice { get; set; }
    }
}