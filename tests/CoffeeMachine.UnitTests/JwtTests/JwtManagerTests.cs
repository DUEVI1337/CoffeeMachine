using System;

using CoffeeMachine.Application;
using CoffeeMachine.Application.Jwt;
using CoffeeMachine.Domain.Entities;

using NUnit.Framework;

namespace CoffeeMachine.UnitTests.JwtTests
{
    [TestFixture]
    public class JwtManagerTests
    {
        [SetUp]
        public void Setup()
        {
            _jwtManager = new JwtManager();
        }

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