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
    public class BigDealTests
    {
        [SetUp]
        public void Setup()
        {
            _dealAlgorithm = new BigDeal();
        }

        private IDeal _dealAlgorithm;

        [Test]
        public void CalcBanknotesDeal_PassData_ReturnCorrectDealAndCashbox()
        {
            //Arrange
            List<BanknoteCashbox> cashboxInit = new()
            {
                new BanknoteCashbox { BanknoteId = Guid.NewGuid(), Denomination = 50, CountBanknote = 100 },
                new BanknoteCashbox { BanknoteId = Guid.NewGuid(), Denomination = 100, CountBanknote = 70 },
                new BanknoteCashbox { BanknoteId = Guid.NewGuid(), Denomination = 200, CountBanknote = 50 }
            };

            var amountDeal = 550;
            List<BanknoteDto> dealExpected = new()
            {
                new BanknoteDto { Denomination = 50, CountBanknote = 1 },
                new BanknoteDto { Denomination = 100, CountBanknote = 1 },
                new BanknoteDto { Denomination = 200, CountBanknote = 2 }
            };
            List<BanknoteCashbox> cashboxExpected = new()
            {
                new BanknoteCashbox { BanknoteId = cashboxInit[0].BanknoteId, Denomination = 50, CountBanknote = 99 },
                new BanknoteCashbox { BanknoteId = cashboxInit[1].BanknoteId, Denomination = 100, CountBanknote = 69 },
                new BanknoteCashbox { BanknoteId = cashboxInit[2].BanknoteId, Denomination = 200, CountBanknote = 48 }
            };

            //Act
            var (dealActual, cashboxActual) = _dealAlgorithm.CalcBanknotesDeal(cashboxInit, amountDeal);

            //Assert
            dealActual.Should().BeEquivalentTo(dealExpected);
            cashboxActual.Should().BeEquivalentTo(cashboxExpected);
        }

        [Test]
        public void CalcBanknotesDeal_PassData_ReturnNullAndNull()
        {
            //Arrange
            List<BanknoteCashbox> cashboxInit = new()
            {
                new BanknoteCashbox { BanknoteId = Guid.NewGuid(), Denomination = 5000, CountBanknote = 1 }
            };

            var amountDeal = 350;

            //Act
            var (dealActual, cashboxActual) = _dealAlgorithm.CalcBanknotesDeal(cashboxInit, amountDeal);

            //Assert
            Assert.That(dealActual, Is.Null);
            Assert.That(cashboxActual, Is.Null);
        }
    }
}