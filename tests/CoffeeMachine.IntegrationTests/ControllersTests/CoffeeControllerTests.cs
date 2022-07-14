using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

using CoffeeMachine.Application;
using CoffeeMachine.Application.Dto;
using CoffeeMachine.Application.Jwt;
using CoffeeMachine.Domain.Entities;
using CoffeeMachine.Infrastructure;

using FluentAssertions;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using NUnit.Framework;

namespace CoffeeMachine.IntegrationTests.ControllersTests
{
    public class CoffeeControllerTests
    {
        private HttpClient _client;
        private DataContext _db;
        private WebAppFactory _factory;

        [Test]
        public async Task BuyCoffee_FindCoffee_ReturnNotFound()
        {
            //Arrange
            List<BanknoteDto> clientMoney = new()
            {
                new BanknoteDto { Denomination = 500, CountBanknote = 1 },
                new BanknoteDto { Denomination = 1000, CountBanknote = 1 },
            };
            OrderDto order = new()
            {
                CoffeeId = Guid.NewGuid().ToString(),
                TypeDeal = 1,
                Banknotes = clientMoney
            };

            //Act
            var result = await _client.PostAsJsonAsync("coffee/v1/BuyCoffee", order);

            //Assert
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task BuyCoffee_GiveDeal_ReturnOk()
        {
            //Arrange
            List<BanknoteDto> clientMoney = new()
            {
                new BanknoteDto { Denomination = 500, CountBanknote = 1 },
                new BanknoteDto { Denomination = 1000, CountBanknote = 1 },
            };
            Coffee coffee = new() { CoffeeId = Guid.NewGuid(), Price = 100, Name = "Latte" };
            OrderDto order = new()
            {
                CoffeeId = coffee.CoffeeId.ToString(),
                TypeDeal = 1,
                Banknotes = clientMoney
            };

            List<BanknoteCashbox> cashbox = new()
            {
                new BanknoteCashbox { BanknoteId = Guid.NewGuid(), Denomination = 100, CountBanknote = 5 },
                new BanknoteCashbox { BanknoteId = Guid.NewGuid(), Denomination = 500, CountBanknote = 3 },
                new BanknoteCashbox { BanknoteId = Guid.NewGuid(), Denomination = 1000, CountBanknote = 1 },
            };
            _db.Coffees.Add(coffee);
            _db.BanknoteCashboxes.AddRange(cashbox);
            await _db.SaveChangesAsync();

            //Act
            var result = await _client.PostAsJsonAsync("coffee/v1/BuyCoffee", order);

            //Assert
            await _db.Database.EnsureDeletedAsync();
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task BuyCoffee_ImpossibleGiveDeal_ReturnBadRequest()
        {
            //Arrange
            List<BanknoteDto> clientMoney = new()
            {
                new BanknoteDto { Denomination = 500, CountBanknote = 1 },
                new BanknoteDto { Denomination = 1000, CountBanknote = 1 },
            };
            Coffee coffee = new() { CoffeeId = Guid.NewGuid(), Price = 100, Name = "Latte" };
            OrderDto order = new()
            {
                CoffeeId = coffee.CoffeeId.ToString(),
                TypeDeal = 1,
                Banknotes = clientMoney
            };
            _db.Coffees.Add(coffee);
            await _db.SaveChangesAsync();

            //Act
            var result = await _client.PostAsJsonAsync("coffee/v1/BuyCoffee", order);

            //Assert
            await _db.Database.EnsureDeletedAsync();
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.InternalServerError));
        }

        [Test]
        public async Task GetCoffeeDtoById_CoffeeNotNull_ReturnOk()
        {
            //Arrange
            var id = Guid.NewGuid();
            Coffee coffee = new() { CoffeeId = id, Price = 100, Name = "Latte" };
            CoffeeDto coffeeExpected = new() { CoffeeId = id.ToString(), CoffeePrice = 100, CoffeeName = "Latte" };
            _db.Coffees.Add(coffee);
            await _db.SaveChangesAsync();

            //Act
            var coffeeActual = await _client.GetFromJsonAsync($"coffee/v1/Coffee/{id}", typeof(CoffeeDto));

            //Assert
            await _db.Database.EnsureDeletedAsync();
            coffeeActual.Should().BeEquivalentTo(coffeeExpected);
        }

        [Test]
        public async Task GetCoffeeDtoById_CoffeeNull_ReturnNotFound()
        {
            //Arrange
            var id = Guid.NewGuid().ToString();

            //Act
            var result = await _client.GetAsync($"coffee/v1/Coffee/{id}");

            //Assert
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task GetListCoffeeDto_ListContainTwoCoffee_ReturnOk()
        {
            //Arrange
            List<Coffee> coffees = new()
            {
                new Coffee { CoffeeId = Guid.NewGuid(), Price = 100, Name = "Latte" },
                new Coffee { CoffeeId = Guid.NewGuid(), Price = 200, Name = "Very Black" }
            };
            _db.Coffees.AddRange(coffees);
            await _db.SaveChangesAsync();
            List<CoffeeDto> coffeesExpected = new()
            {
                new CoffeeDto
                {
                    CoffeeId = coffees[0].CoffeeId.ToString(), CoffeePrice = coffees[0].Price,
                    CoffeeName = coffees[0].Name
                },
                new CoffeeDto
                {
                    CoffeeId = coffees[1].CoffeeId.ToString(), CoffeePrice = coffees[1].Price,
                    CoffeeName = coffees[1].Name
                },
            };

            //Act
            var coffeesActual = await _client.GetFromJsonAsync("coffee/v1/CoffeeMenu", typeof(List<CoffeeDto>));

            //Assert
            await _db.Database.EnsureDeletedAsync();
            coffeesActual.Should().BeEquivalentTo(coffeesExpected);
        }

        [SetUp]
        public void Setup()
        {
            _factory = new WebAppFactory();
            _db = _factory.Services.CreateScope().ServiceProvider.GetService<DataContext>();
            _db.Database.EnsureDeleted(); //clear init data from db
            var config = _factory.Services.GetService<IConfiguration>();
            JwtManager jwtManager = new(config);
            User user = new()
            {
                IdUser = Guid.NewGuid(),
                Username = "qwe",
                Password = PasswordProtect.GetPasswordProtect("qwe123")
            };
            _db.Users.Add(user);
            _db.SaveChanges();
            _client = _factory.CreateClient();
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", jwtManager.GenerateJwtToken(user));
        }
    }
}