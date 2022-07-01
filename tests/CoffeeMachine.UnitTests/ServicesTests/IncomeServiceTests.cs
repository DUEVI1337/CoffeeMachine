using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using CoffeeMachine.Application.Services;
using CoffeeMachine.Domain.Dto;
using CoffeeMachine.Domain.Entities;
using CoffeeMachine.Infrastructure;
using CoffeeMachine.Infrastructure.Repositories;

using FluentAssertions;

using Microsoft.EntityFrameworkCore;

using NUnit.Framework;

namespace CoffeeMachine.UnitTests.ServicesTests
{
    public class IncomeServiceTests
    {
        private IncomeService _incomeService;
        private DataContext _db;

        [SetUp]
        public void Setup()
        {
            var _dbOptions = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            _db = new DataContext(_dbOptions);
            var uow = new UnitOfWork(_db, new CoffeeRepository(_db), new BalanceRepository(_db),
                new BanknoteCashboxRepository(_db), new PaymentRepository(_db), new IncomeRepository(_db));
            _incomeService = new IncomeService(uow);
        }

        [Test]
        public async Task AddIncomeAsync_CheckNewIncomeInDb_AddNewIncomeInDb()
        {
            //Arrange
            const int coffeePrice = 100;
            const int numberIncomeExpected = 1;

            //Act
            await _incomeService.AddIncomeAsync(coffeePrice);
            await _db.SaveChangesAsync();

            //Assert
            int numberIncomeActual = _db.Incomes.ToList().Count;
            await _db.DisposeAsync();
            Assert.That(numberIncomeActual, Is.EqualTo(numberIncomeExpected));
        }

        [Test]
        public async Task AddIncomeAsync_CheckUpdateIncomeInDb_UpdateIncomeInDb()
        {
            //Arrange
            const int coffeePrice = 100;
            _db.Incomes.Add(new Income
            {
                IncomeId = Guid.NewGuid(),
                Date = DateTime.UtcNow,
                TotalIncome = coffeePrice
            });
            await _db.SaveChangesAsync();
            const int totalIncomeExpected = 200;

            //Act
            await _incomeService.AddIncomeAsync(coffeePrice);
            await _db.SaveChangesAsync();

            //Assert
            int totslIncomeActual = _db.Incomes.FirstOrDefault(x => x.Date.Day == DateTime.UtcNow.Day).TotalIncome;
            await _db.DisposeAsync();
            Assert.That(totslIncomeActual, Is.EqualTo(totalIncomeExpected));
        }
    }
}
