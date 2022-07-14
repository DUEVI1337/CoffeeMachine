using System;
using System.Collections.Generic;

using CoffeeMachine.Application.Dto;
using CoffeeMachine.Application.Strategy.Base;
using CoffeeMachine.Application.Strategy.Strategies;
using CoffeeMachine.Domain.Entities;

using FluentAssertions;

using NUnit.Framework;

namespace CoffeeMachine.UnitTests.StrategyTests.StrategiesTests
{
    [TestFixture]
    public class EvenlyDealTests
    {
        [SetUp]
        public void Setup()
        {
            _dealAlgorithm = new EvenlyDeal();
        }

        private IDeal _dealAlgorithm;

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
            var (dealActual, cashboxActual) = _dealAlgorithm.GetDeal(cashboxInit, amountDeal);

            //Assert
            Assert.That(dealActual, Is.Null);
            Assert.That(cashboxActual, Is.Null);
        }

        [Test]
        public void GetDeal_PossibleGiveDeal_ReturnCorrectDealAndCashbox()
        {
            //Arrange
            List<BanknoteCashbox> cashboxInit = new()
            {
                new BanknoteCashbox { BanknoteId = Guid.NewGuid(), Denomination = 50, CountBanknote = 10 },
                new BanknoteCashbox { BanknoteId = Guid.NewGuid(), Denomination = 100, CountBanknote = 20 },
                new BanknoteCashbox { BanknoteId = Guid.NewGuid(), Denomination = 200, CountBanknote = 10 }
            };

            const int amountDeal = 250;
            List<BanknoteDto> dealExpected = new()
            {
                new BanknoteDto { Denomination = 200, CountBanknote = 1 },
                new BanknoteDto { Denomination = 50, CountBanknote = 1 },
            };
            List<BanknoteCashbox> cashboxExpected = new()
            {
                new BanknoteCashbox { BanknoteId = cashboxInit[2].BanknoteId, Denomination = 200, CountBanknote = 9 },
                new BanknoteCashbox { BanknoteId = cashboxInit[1].BanknoteId, Denomination = 100, CountBanknote = 20 },
                new BanknoteCashbox { BanknoteId = cashboxInit[0].BanknoteId, Denomination = 50, CountBanknote = 9 }
            };

            //Act
            var (dealActual, cashboxActual) = _dealAlgorithm.GetDeal(cashboxInit, amountDeal);

            //Assert
            dealActual.Should().BeEquivalentTo(dealExpected);
            cashboxActual.Should().BeEquivalentTo(cashboxExpected);
        }
    }
}
