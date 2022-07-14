using System;
using System.Threading.Tasks;

using CoffeeMachine.Application;
using CoffeeMachine.Application.Dto;
using CoffeeMachine.Application.Exceptions.CustomExceptions;
using CoffeeMachine.Application.Jwt;
using CoffeeMachine.Application.Services;
using CoffeeMachine.Domain.Entities;
using CoffeeMachine.Infrastructure;
using CoffeeMachine.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;

using NUnit.Framework;

namespace CoffeeMachine.UnitTests.ServicesTests
{
    [TestFixture]
    public class AccountServiceTests
    {
        [SetUp]
        public void Setup()
        {
            var dbOpt = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var db = new DataContext(dbOpt);
            _uow = new UnitOfWork(db, new CoffeeRepository(db), new BalanceRepository(db),
                new BanknoteCashboxRepository(db), new PaymentRepository(db), new IncomeRepository(db),
                new UserRepository(db));
            _jwtManager = new JwtManager();
            _accountService = new AccountService(_uow, _jwtManager);
        }

        private JwtManager _jwtManager;
        private UnitOfWork _uow;
        private AccountService _accountService;

        [Test]
        public async Task RegisterAccountAsync_NotUniqueUsername_ReturnException()
        {
            //Arrange
            RegisterDto dto = new() { Username = "qwe", Password = "qwe123" };
            User user = new() { Username = "qwe", Password = "123456" };
            _uow.UserRepo.Add(user);
            await _uow.SaveChangesAsync();

            //Act && Assert
            Assert.ThrowsAsync<UsernameNotUniqueException>(() => _accountService.RegisterAccountAsync(dto));
        }

        [Test]
        public async Task RegisterAccountAsync_ValidDto_NumberUserInDb1()
        {
            //Arrange
            RegisterDto dto = new() { Username = "qwe", Password = "qwe123" };

            //Act
            await _accountService.RegisterAccountAsync(dto);

            //Assert
            var users = await _uow.UserRepo.GetAllAsync();
            Assert.That(users.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task SignInAccountAsync_InvalidPassword_ReturnException()
        {
            //Assert
            SignInDto dto = new() { Username = "qwe", Password = "qwe123" };
            User user = new()
            {
                IdUser = Guid.NewGuid(),
                Username = dto.Username,
                Password = PasswordProtect.GetPasswordProtect("111111")
            };
            _uow.UserRepo.Add(user);
            await _uow.SaveChangesAsync();

            //Act && Assert
            Assert.ThrowsAsync<SignInFailException>(() => _accountService.SignInAccountAsync(dto));
        }

        [Test]
        public async Task SignInAccountAsync_InvalidUsername_ReturnException()
        {
            //Assert
            SignInDto dto = new() { Username = "qwe", Password = "qwe123" };
            User user = new()
            {
                IdUser = Guid.NewGuid(),
                Username = "111",
                Password = PasswordProtect.GetPasswordProtect(dto.Password)
            };
            _uow.UserRepo.Add(user);
            await _uow.SaveChangesAsync();

            //Act && Assert
            Assert.ThrowsAsync<SignInFailException>(() => _accountService.SignInAccountAsync(dto));
        }

        [Test]
        public async Task SignInAccountAsync_ValidDto_ReturnJwtToken()
        {
            //Assert
            SignInDto dto = new() { Username = "qwe", Password = "qwe123" };
            User user = new()
            {
                IdUser = Guid.NewGuid(),
                Username = dto.Username,
                Password = PasswordProtect.GetPasswordProtect(dto.Password)
            };
            _uow.UserRepo.Add(user);
            await _uow.SaveChangesAsync();

            //Act
            var token = _accountService.SignInAccountAsync(dto);

            //Assert
            Assert.NotNull(token);
        }
    }
}