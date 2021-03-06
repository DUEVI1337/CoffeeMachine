using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CoffeeMachine.Application.Dto;
using CoffeeMachine.Application.Services;
using CoffeeMachine.Domain.Entities;
using CoffeeMachine.Infrastructure;
using CoffeeMachine.Infrastructure.Repositories;

using FluentAssertions;

using Microsoft.EntityFrameworkCore;

using NUnit.Framework;

namespace CoffeeMachine.UnitTests.ServicesTests
{
    public class CoffeeServiceTests
    {
        private CoffeeService _coffeeService;
        private DataContext _db;

        [Test]
        public async Task BuyCoffee_CheckDeal_ReturnCorrectDeal()
        {
            //Arrange
            Coffee coffee = new()
            {
                Price = 100,
                CoffeeId = Guid.NewGuid(),
                Name = "Latte"
            };
            List<BanknoteDto> clientMoney = new()
            {
                new BanknoteDto { CountBanknote = 1, Denomination = 500 },
                new BanknoteDto { CountBanknote = 1, Denomination = 1000 },
            };
            List<BanknoteCashbox> cashbox = new()
            {
                new BanknoteCashbox { CountBanknote = 30, Denomination = 100, BanknoteId = Guid.NewGuid() },
                new BanknoteCashbox { CountBanknote = 20, Denomination = 200, BanknoteId = Guid.NewGuid() },
                new BanknoteCashbox { CountBanknote = 10, Denomination = 500, BanknoteId = Guid.NewGuid() },
                new BanknoteCashbox { CountBanknote = 5, Denomination = 1000, BanknoteId = Guid.NewGuid() }
            };
            var typeDeal = TypeDeal.BigDeal;
            _db.Coffees.Add(coffee);
            _db.BanknotesCashbox.AddRange(cashbox);
            await _db.SaveChangesAsync();
            const int amountDealExpected = 1400;

            //Act
            var deal = await _coffeeService.BuyCoffeeAsync(coffee.CoffeeId.ToString(), clientMoney, typeDeal);

            //Assert
            await _db.DisposeAsync();
            var amountDealActual = deal.Sum(x => x.Denomination * x.CountBanknote);
            Assert.That(amountDealActual, Is.EqualTo(amountDealExpected));
        }

        [Test]
        public async Task BuyCoffee_UseDynamicDeal_ReturnCorrectDeal()
        {
            //Arrange
            Coffee coffee = new()
            {
                Price = 1000,
                CoffeeId = Guid.NewGuid(),
                Name = "Latte"
            };
            List<BanknoteCashbox> cashbox = new()
            {
                new BanknoteCashbox { BanknoteId = Guid.NewGuid(), Denomination = 5000, CountBanknote = 1 },
                new BanknoteCashbox { BanknoteId = Guid.NewGuid(), Denomination = 2000, CountBanknote = 2 }
            };

            List<BanknoteDto> clientMoney = new()
            {
                new BanknoteDto { CountBanknote = 1, Denomination = 5000 },
                new BanknoteDto { CountBanknote = 1, Denomination = 2000 },
            };
            List<BanknoteDto> dealExpected = new()
            {
                new BanknoteDto { Denomination = 2000, CountBanknote = 3 },
            };
            var typeDeal = TypeDeal.BigDeal;
            _db.Coffees.Add(coffee);
            _db.BanknotesCashbox.AddRange(cashbox);
            await _db.SaveChangesAsync();

            //Act
            var dealActual = await _coffeeService.BuyCoffeeAsync(coffee.CoffeeId.ToString(), clientMoney, typeDeal);

            //Assert
            dealExpected.Should().BeEquivalentTo(dealActual);
        }

        [Test]
        public async Task GetCoffeeDtoByIdAsync_AddCoffeeInDb_ReturnCoffeeDto()
        {
            //Arrange
            List<Coffee> coffeeInDb = new()
            {
                new Coffee { CoffeeId = Guid.NewGuid(), Name = "Very Black", Price = 100 },
                new Coffee { CoffeeId = Guid.NewGuid(), Name = "Latte", Price = 200 },
            };
            _db.Coffees.AddRange(coffeeInDb);
            await _db.SaveChangesAsync();
            CoffeeDto coffeeDtoExpected = new()
            {
                CoffeeId = coffeeInDb[0].CoffeeId.ToString(),
                CoffeePrice = coffeeInDb[0].Price,
                CoffeeName = coffeeInDb[0].Name
            };

            //Act
            var coffeeDtoActual = await _coffeeService.GetCoffeeDtoByIdAsync(coffeeInDb[0].CoffeeId.ToString());

            //Assert
            await _db.DisposeAsync();
            coffeeDtoActual.Should().BeEquivalentTo(coffeeDtoExpected);
        }

        [Test]
        public async Task GetListCoffeeDtoAsync_AddListCoffeeInDb_ReturnCoffeeDto()
        {
            //Arrange
            List<Coffee> coffeeInDb = new()
            {
                new Coffee { CoffeeId = Guid.NewGuid(), Name = "Very Black", Price = 100 },
                new Coffee { CoffeeId = Guid.NewGuid(), Name = "Latte", Price = 200 },
            };
            _db.Coffees.AddRange(coffeeInDb);
            await _db.SaveChangesAsync();
            List<CoffeeDto> coffeesDtoExpected = new()
            {
                new CoffeeDto
                {
                    CoffeeId = coffeeInDb[0].CoffeeId.ToString(), CoffeeName = coffeeInDb[0].Name,
                    CoffeePrice = coffeeInDb[0].Price
                },
                new CoffeeDto
                {
                    CoffeeId = coffeeInDb[1].CoffeeId.ToString(), CoffeeName = coffeeInDb[1].Name,
                    CoffeePrice = coffeeInDb[1].Price
                }
            };

            //Act
            var coffeesDtoActual = await _coffeeService.GetListCoffeeDtoAsync();

            //Assert
            await _db.DisposeAsync();
            coffeesDtoActual.Should().BeEquivalentTo(coffeesDtoExpected);
        }

        [SetUp]
        public void Setup()
        {
            var dbOptions = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            _db = new DataContext(dbOptions);
            _db.Database.EnsureDeleted();
            var uow = new UnitOfWork(_db, new CoffeeRepository(_db), new BalanceRepository(_db),
                new BanknoteCashboxRepository(_db), new PaymentRepository(_db), new IncomeRepository(_db),
                new UserRepository(_db));
            _coffeeService = new CoffeeService(uow, new BanknoteCashboxService(uow), new BalanceService(uow),
                new PaymentService(uow), new IncomeService(uow));
        }
    }
}
