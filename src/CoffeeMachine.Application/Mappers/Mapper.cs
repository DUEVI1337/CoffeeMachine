using CoffeeMachine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CoffeeMachine.Domain.DTO;

namespace CoffeeMachine.Application.Mappers
{
    /// <summary>
    /// map entities to DTO
    /// </summary>
    public class Mapper
    {
        /// <summary>
        /// map 'Coffee' to 'CoffeeDto'
        /// </summary>
        /// <param name="coffee"></param>
        /// <returns><see cref="CoffeeDto"/></returns> 
        public static CoffeeDto CoffeeMapper(Coffee coffee) =>
            new () { CoffeeId = coffee.CoffeeId.ToString(), CoffeeName = coffee.Name, CoffeePrice = coffee.Price };

        /// <summary>
        /// map list 'Coffee' to list 'CoffeeDto'
        /// </summary>
        /// <param name="coffees"></param>
        /// <returns><see cref="List{T}"/> where T <see cref="CoffeeDto"/></returns>
        public static List<CoffeeDto> CoffeeListMapper(List<Coffee> coffees)
        {
            List<CoffeeDto> coffeesDto = new List<CoffeeDto>();
            coffees.ForEach(x=>coffeesDto.Add(CoffeeMapper(x)));
            return coffeesDto;
        }

    }
}
