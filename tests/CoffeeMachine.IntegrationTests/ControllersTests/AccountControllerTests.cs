using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

using CoffeeMachine.Application;
using CoffeeMachine.Application.Dto;
using CoffeeMachine.Domain.Entities;
using CoffeeMachine.Infrastructure;

using Microsoft.Extensions.DependencyInjection;

using NUnit.Framework;

namespace CoffeeMachine.IntegrationTests.ControllersTests
{
    public class AccountControllerTests
    {
        private HttpClient _client;
        private DataContext _db;
        private WebAppFactory _factory;

        [Test]
        public async Task RegisterAccount_InvalidDto_ReturnStatusCode400()
        {
            //Arrange
            RegisterDto dto = new()
            {
                Username = "qwe",
                Password = "qwe12", //min 6 char
                PasswordConfirm = "qwe" // PasswordConfirm == password
            };

            //Act
            var result = await _client.PostAsJsonAsync("account/v1/Register", dto);

            //Assert
            await _db.Database.EnsureDeletedAsync();
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task RegisterAccount_ValidDto_ReturnStatusCode200()
        {
            //Arrange
            RegisterDto dto = new()
            {
                Username = "qwe",
                Password = "qwe123",
                PasswordConfirm = "qwe123"
            };

            //Act
            var result = await _client.PostAsJsonAsync("account/v1/Register", dto);

            //Assert
            await _db.Database.EnsureDeletedAsync();
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [SetUp]
        public void Setup()
        {
            _factory = new WebAppFactory();
            _db = _factory.Services.CreateScope().ServiceProvider.GetService<DataContext>();
            _db.Database.EnsureDeleted(); //clear init data from db
            _client = _factory.CreateClient();
        }

        [Test]
        public async Task SignInAccount_InvalidData_ReturnStatusCode400()
        {
            //Arrange
            SignInDto dto = new()
            {
                Username = "qwe2213",
                Password = "qwe12331234",
            };
            User user = new()
            {
                IdUser = Guid.NewGuid(),
                Username = "qwe",
                Password = PasswordProtect.GetPasswordProtect("qwe123")
            };
            _db.Add(user);
            await _db.SaveChangesAsync();

            //Act
            var result = await _client.PostAsJsonAsync("account/v1/SignIn", dto);

            //Assert
            await _db.Database.EnsureDeletedAsync();
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task SignInAccount_ValidData_ReturnStatusCode200()
        {
            //Arrange
            SignInDto dto = new()
            {
                Username = "qwe",
                Password = "qwe123",
            };
            User user = new()
            {
                IdUser = Guid.NewGuid(),
                Username = dto.Username,
                Password = PasswordProtect.GetPasswordProtect(dto.Password)
            };
            _db.Add(user);
            await _db.SaveChangesAsync();

            //Act
            var result = await _client.PostAsJsonAsync("account/v1/SignIn", dto);

            //Assert
            await _db.Database.EnsureDeletedAsync();
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}