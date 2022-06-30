using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using CoffeeMachine.Application.Service;
using CoffeeMachine.Domain.Dto;
using CoffeeMachine.Domain.Entities;
using CoffeeMachine.Infrastructure;
using CoffeeMachine.Infrastructure.Repositories;

using FluentAssertions;

using Microsoft.EntityFrameworkCore;

using NUnit.Framework;

namespace CoffeeMachine.UnitTests.ServicesTests
{
    public class BalanceServiceTest
    {
        private BalanceService _balanceService;
        private DataContext _db;

        [SetUp]
        public void Setup()
        {
            var _dbOptions = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            _db = new DataContext(_dbOptions);
            var uow = new UnitOfWork(_db, new CoffeeRepository(_db), new BalanceRepository(_db),
                new BanknoteCashboxRepository(_db), new PaymentRepository(_db), new IncomeRepository(_db));
            _balanceService = new BalanceService(uow);
        }

        [Test]
        public async Task GetBalancesDtoAsync_CheckBalanceFromDb_ReturnListBalanceDto()
        {
            //Arrange
            const int earnedMoney = 100;
            List<Balance> balancesInDb = new ()
            {
                new Balance {CoffeeId = Guid.NewGuid(), BalanceId = Guid.NewGuid(), EarnedMoney = earnedMoney},
                new Balance {CoffeeId = Guid.NewGuid(), BalanceId = Guid.NewGuid(), EarnedMoney = earnedMoney},
            };
            _db.Balances.AddRange(balancesInDb);
            await _db.SaveChangesAsync();
            List<BalanceDto> balancesDtoExpected = new ()
            {
                new BalanceDto {CoffeeId = balancesInDb[0].CoffeeId, BalanceId = balancesInDb[0].BalanceId, EarnedMoney = earnedMoney},
                new BalanceDto {CoffeeId = balancesInDb[1].CoffeeId, BalanceId = balancesInDb[1].BalanceId, EarnedMoney = earnedMoney},
            };

            //Act
            List<BalanceDto> balancesDtoActual = await _balanceService.GetBalancesDtoAsync();

            //Assert
            await _db.DisposeAsync();
            balancesDtoActual.Should().BeEquivalentTo(balancesDtoExpected);
        }

        [Test]
        public async Task UpdateBalanceAsync_CheckUpdateBalanceInDb_AddNewBalanceInDb()
        {
            //Arrange
            string coffeeId = Guid.NewGuid().ToString();
            const int coffeePrice = 100;
            const int numberBalanceExpected = 1;

            //Act
            await _balanceService.UpdateBalanceAsync(coffeeId, coffeePrice);
            await _db.SaveChangesAsync();

            //Assert
            int numberBalanceActual = _db.Balances.ToList().Count;
            await _db.DisposeAsync();
            Assert.AreEqual(numberBalanceExpected, numberBalanceActual);
        }

        [Test]
        public async Task UpdateBalanceAsync_CheckUpdateBalanceInDb_UpdateBalanceInDb()
        {
            //Arrange
            string coffeeId = Guid.NewGuid().ToString();
            const int coffeePrice = 100;
            _db.Balances.Add(new Balance
            {
                BalanceId = Guid.NewGuid(), CoffeeId = Guid.Parse(coffeeId), EarnedMoney = coffeePrice
            });
            await _db.SaveChangesAsync();
            const int earnedMoneyBalanceExpected = 200;

            //Act
            await _balanceService.UpdateBalanceAsync(coffeeId, coffeePrice);
            await _db.SaveChangesAsync();

            //Assert
            int earnedMoneyBalanceActual = _db.Balances.FirstOrDefault(x=>x.CoffeeId == Guid.Parse(coffeeId)).EarnedMoney;
            await _db.DisposeAsync();
            Assert.AreEqual(earnedMoneyBalanceExpected, earnedMoneyBalanceActual);
        }
    }
}
