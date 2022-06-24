using System.Collections.Generic;
using System.Linq;

using CoffeeMachine.Domain.DTO;
using CoffeeMachine.Domain.Entities;

namespace CoffeeMachine.Application.Mappers
{
    /// <summary>
    /// map entities to DTO
    /// </summary>
    public class Mapper
    {
        /// <summary>
        /// map list 'Coffee' to list 'CoffeeDto'
        /// </summary>
        /// <param name="coffees"></param>
        /// <returns><see cref="List{T}"/> where T <see cref="CoffeeDto"/></returns>
        public static List<CoffeeDto> MapToListCoffeeDto(List<Coffee> coffees)
        {
            return coffees.Select(x => MapToCoffeeDto(x)).ToList();
        }

        /// <summary>
        /// map 'Coffee' to 'CoffeeDto'
        /// </summary>
        /// <param name="coffee"></param>
        /// <returns><see cref="CoffeeDto"/></returns> 
        public static CoffeeDto MapToCoffeeDto(Coffee coffee)
        {
            return new()
                { CoffeeId = coffee.CoffeeId.ToString(), CoffeeName = coffee.Name, CoffeePrice = coffee.Price };
        }
    }
}