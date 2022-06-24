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
    /// 
    /// </summary>
    public class Mapper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="coffee"></param>
        /// <returns></returns> 
        public static CoffeeDto CoffeeMapper(Coffee coffee) =>
            new () { CoffeeId = coffee.CoffeeId.ToString(), CoffeeName = coffee.Name, CoffeePrice = coffee.Price };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="coffees"></param>
        /// <returns></returns>
        public static List<CoffeeDto> CoffeeListMapper(List<Coffee> coffees)
        {
            List<CoffeeDto> coffeesDto = new List<CoffeeDto>();
            coffees.ForEach(x=>coffeesDto.Add(CoffeeMapper(x)));
            return coffeesDto;
        }

    }
}
