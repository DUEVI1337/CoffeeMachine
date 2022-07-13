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

        //[Test]
        //public void CalcBanknotesDeal_UseLongAlgorithm_ReturnCorrectDealAndCashbox()
        //{
        //    //Arrange
        //    List<BanknoteCashbox> cashboxInit = new()
        //    {
        //        new BanknoteCashbox { BanknoteId = Guid.NewGuid(), Denomination = 5000, CountBanknote = 1 },
        //        new BanknoteCashbox { BanknoteId = Guid.NewGuid(), Denomination = 2000, CountBanknote = 3 }
        //    };

        //    var amountDeal = 6000;
        //    List<BanknoteDto> dealExpected = new()
        //    {
        //        new BanknoteDto { Denomination = 2000, CountBanknote = 3 },
        //    };
        //    List<BanknoteCashbox> cashboxExpected = new()
        //    {
        //        new BanknoteCashbox { BanknoteId = cashboxInit[0].BanknoteId, Denomination = 5000, CountBanknote = 1 },
        //        new BanknoteCashbox { BanknoteId = cashboxInit[1].BanknoteId, Denomination = 2000, CountBanknote = 0 },
        //    };

        //    //Act
        //    var (dealActual, cashboxActual) = _dealAlgorithm.GetDeal(cashboxInit, amountDeal);

        //    //Assert
        //    dealExpected.Should().BeEquivalentTo(dealActual);
        //    cashboxExpected.Should().BeEquivalentTo(cashboxActual);
        //}

        [Test]
        public void CalcBanknotesDeal_PassData_ReturnCorrectDealAndCashbox()
        {
            //Arrange
            List<BanknoteCashbox> cashboxInit = new()
            {
                new BanknoteCashbox { BanknoteId = Guid.NewGuid(), Denomination = 5000, CountBanknote = 1 },
                new BanknoteCashbox { BanknoteId = Guid.NewGuid(), Denomination = 2000, CountBanknote = 2 },
                new BanknoteCashbox { BanknoteId = Guid.NewGuid(), Denomination = 1000, CountBanknote = 1 },
            };

            var amountDeal = 6000;
            List<BanknoteDto> dealExpected = new()
            {
                new BanknoteDto { Denomination = 5000, CountBanknote = 1 },
                new BanknoteDto { Denomination = 1000, CountBanknote = 1 },
            };
            List<BanknoteCashbox> cashboxExpected = new()
            {
                new BanknoteCashbox { BanknoteId = cashboxInit[0].BanknoteId, Denomination = 5000, CountBanknote = 0 },
                new BanknoteCashbox { BanknoteId = cashboxInit[1].BanknoteId, Denomination = 2000, CountBanknote = 2 },
                new BanknoteCashbox { BanknoteId = cashboxInit[2].BanknoteId, Denomination = 1000, CountBanknote = 0 }
            };

            //Act
            var (dealActual, cashboxActual) = _dealAlgorithm.GetDeal(cashboxInit, amountDeal);

            //Assert
            dealActual.Should().BeEquivalentTo(dealExpected);
            cashboxExpected.Should().BeEquivalentTo(cashboxActual);
        }

        [Test]
        public void CalcBanknotesDeal_PassData_ReturnNull()
        {
            //Arrange
            List<BanknoteCashbox> cashboxInit = new()
            {
                new BanknoteCashbox { BanknoteId = Guid.NewGuid(), Denomination = 2000, CountBanknote = 2 },
                new BanknoteCashbox { BanknoteId = Guid.NewGuid(), Denomination = 5000, CountBanknote = 1 }
            };

            var amountDeal = 6000;

            //Act
            var (dealActual, cashboxActual) = _dealAlgorithm.GetDeal(cashboxInit, amountDeal);

            //Assert
            Assert.That(dealActual, Is.Null);
            Assert.That(cashboxActual, Is.Null);
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
            var (dealActual, cashboxActual) = _dealAlgorithm.GetDeal(cashboxInit, amountDeal);

            //Assert
            Assert.That(dealActual, Is.Null);
            Assert.That(cashboxActual, Is.Null);
        }
    }
}