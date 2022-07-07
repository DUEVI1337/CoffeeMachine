using System;

using CoffeeMachine.Application.Mappers;
using CoffeeMachine.Domain.Dto;
using CoffeeMachine.Domain.Entities;

using FluentAssertions;

using NUnit.Framework;

namespace CoffeeMachine.IntegrationTests
{
    [TestFixture]
    public class MapperTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void MapToBalanceDto_BalanceObj_ActualDtoEquivalentExpectedDto()
        {
            //Arrange
            const int earnedMoney = 850;
            Balance balanceConvertToDto = new()
                { BalanceId = Guid.NewGuid(), CoffeeId = Guid.NewGuid(), EarnedMoney = earnedMoney };
            BalanceDto balanceDtoExpected = new()
            {
                BalanceId = balanceConvertToDto.BalanceId, CoffeeId = balanceConvertToDto.CoffeeId,
                EarnedMoney = earnedMoney
            };

            //Act
            var balanceDtoActual = Mapper.MapToBalanceDto(balanceConvertToDto);

            //Assert
            balanceDtoActual.Should().BeEquivalentTo(balanceDtoExpected);
        }

        [Test]
        public void MapToCoffeeDto_CoffeeObj_ActualDtoEquivalentExpectedDto()
        {
            const int priceCoffee = 850;
            Coffee coffeeConvertToDto = new() { CoffeeId = Guid.NewGuid(), Name = "Latte", Price = priceCoffee };
            CoffeeDto coffeeDtoExpected = new()
            {
                CoffeeId = coffeeConvertToDto.CoffeeId.ToString(), CoffeeName = coffeeConvertToDto.Name,
                CoffeePrice = priceCoffee
            };

            //Act
            var coffeeDtoActual = Mapper.MapToCoffeeDto(coffeeConvertToDto);

            //Assert
            coffeeDtoActual.Should().BeEquivalentTo(coffeeDtoExpected);
        }
    }
}