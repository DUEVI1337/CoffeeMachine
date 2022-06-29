namespace CoffeeMachine.Domain.Dto
{
    /// <summary>
    /// represent 'coffee' in database
    /// </summary>
    public class CoffeeDto
    {
        /// <summary>
        /// id 'coffee' in database, consists of 32 characters (GUID)
        /// </summary>
        /// <example>6F9619FF-8B86-D011-B42D-00CF4FC964FF</example>
        public string CoffeeId { get; set; }

        /// <summary>
        /// name of coffee
        /// </summary>
        /// <example>Капучино</example>
        public string CoffeeName { get; set; }

        /// <summary>
        /// price of coffee
        /// </summary>
        /// <example>240</example>
        public int CoffeePrice { get; set; }
    }
}