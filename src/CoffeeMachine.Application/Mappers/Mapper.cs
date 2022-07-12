using CoffeeMachine.Application.Dto;
using CoffeeMachine.Domain.Entities;

namespace CoffeeMachine.Application.Mappers
{
    /// <summary>
    /// map entities to DTO
    /// </summary>
    public class Mapper
    {
        /// <summary>
        /// Convert <see cref="Balance"/> to <see cref="BalanceDto"/>
        /// </summary>
        /// <param name="balance"></param>
        /// <returns><see cref="BalanceDto"/></returns>
        public static BalanceDto MapToBalanceDto(Balance balance)
        {
            return new()
            {
                BalanceId = balance.BalanceId, EarnedMoney = balance.EarnedMoney, CoffeeId = balance.CoffeeId
            };
        }

        /// <summary>
        /// Convert <see cref="Coffee"/> to <see cref="CoffeeDto"/>
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