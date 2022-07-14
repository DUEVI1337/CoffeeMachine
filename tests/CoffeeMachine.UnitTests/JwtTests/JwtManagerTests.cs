using System;
using System.Collections.Generic;

using CoffeeMachine.Application;
using CoffeeMachine.Application.Jwt;
using CoffeeMachine.Domain.Entities;

using Microsoft.Extensions.Configuration;

using NUnit.Framework;

namespace CoffeeMachine.UnitTests.JwtTests
{
    [TestFixture]
    public class JwtManagerTests
    {
        [SetUp]
        public void Setup()
        {
            var appSettings = new Dictionary<string, string>
            {
                { "Jwt:Key", "111111111111111111111111" },
                { "Jwt:Issuer", "Test" },
                { "Jwt:ExpirationTime", "30" }
            };

            _config = new ConfigurationBuilder()
                .AddInMemoryCollection(appSettings)
                .Build();

            _jwtManager = new JwtManager(_config);
        }

        private IConfiguration _config;

        private JwtManager _jwtManager;

        [Test]
        public void GenerateJwtToken_GenerateTokenForUser_ReturnJwtToken()
        {
            //Arrange
            User user = new()
            {
                IdUser = Guid.NewGuid(),
                Username = "qwe",
                Password = PasswordProtect.GetPasswordProtect("qwe123")
            };

            //Act
            var token = _jwtManager.GenerateJwtToken(user);

            //Assert
            Assert.NotNull(token);
        }
    }
}