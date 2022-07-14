using CoffeeMachine.Application.Dto;
using CoffeeMachine.Application.Strategy.Base;
using CoffeeMachine.Application.Strategy.Strategies;
using CoffeeMachine.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CoffeeMachine.UnitTests.StrategyTests.StrategiesTests
{
    [TestFixture]
    public class DynamicDealTests
    {
        private IDeal _algorithmDeal;

        [SetUp]
        public void Setup()
        {
            _algorithmDeal = new DynamicDeal();
        }

        [Test]
        public void GetDeal_PossibleGiveDeal_ReturnCashboxAndDeal()
        {
            //Arrange
            List<BanknoteCashbox> cashboxInit = new()
                {
                    new BanknoteCashbox { BanknoteId = Guid.NewGuid(), Denomination = 5000, CountBanknote = 1 },
                    new BanknoteCashbox { BanknoteId = Guid.NewGuid(), Denomination = 2000, CountBanknote = 3 }
                };

            const int amountDeal = 6000;
            List<BanknoteDto> dealExpected = new()
                {
                    new BanknoteDto { Denomination = 2000, CountBanknote = 3 },
                };
            List<BanknoteCashbox> cashboxExpected = new()
                {
                    new BanknoteCashbox { BanknoteId = cashboxInit[0].BanknoteId, Denomination = 5000, CountBanknote = 1 },
                    new BanknoteCashbox { BanknoteId = cashboxInit[1].BanknoteId, Denomination = 2000, CountBanknote = 0 },
                };

            //Act
            var (dealActual, cashboxActual) = _algorithmDeal.GetDeal(cashboxInit, amountDeal);

            //Assert
            dealExpected.Should().BeEquivalentTo(dealActual);
            cashboxExpected.Should().BeEquivalentTo(cashboxActual);
        }

        [Test]
        public void GetDeal_ImpossibleGiveDeal_ReturnNullAndNull()
        {
            //Arrange
            List<BanknoteCashbox> cashboxInit = new()
            {
                new BanknoteCashbox { BanknoteId = Guid.NewGuid(), Denomination = 5000, CountBanknote = 1 }
            };

            const int amountDeal = 350;

            //Act
            var (dealActual, cashboxActual) = _algorithmDeal.GetDeal(cashboxInit, amountDeal);

            //Assert
            Assert.That(dealActual, Is.Null);
            Assert.That(cashboxActual, Is.Null);
        }
    }
}
