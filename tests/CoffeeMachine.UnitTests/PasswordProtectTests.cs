using System;
using System.Security.Cryptography;

using CoffeeMachine.Application;

using FluentAssertions;

using NUnit.Framework;

namespace CoffeeMachine.UnitTests
{
    [TestFixture]
    public class PasswordProtectTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CheckPassword_InvalidPassword_ReturnFalse()
        {
            //Arrange
            var passwordInvalid = "123";
            var password = "qwe123";
            const int saltSize = 8;
            const int keySize = 16;
            const int iterations = 1000;
            using var encrypt = new Rfc2898DeriveBytes(
                password,
                saltSize,
                iterations,
                HashAlgorithmName.SHA256);
            var key = Convert.ToBase64String(encrypt.GetBytes(keySize));
            var salt = Convert.ToBase64String(encrypt.Salt);

            //Act
            var result = PasswordProtect.CheckPassword($"{iterations}!{salt}!{key}", passwordInvalid);

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CheckPassword_ValidPassword_ReturnTrue()
        {
            //Arrange
            var password = "qwe123";
            var saltSize = 8;
            var keySize = 16;
            var iterations = 1000;
            using var encrypt = new Rfc2898DeriveBytes(
                password,
                saltSize,
                iterations,
                HashAlgorithmName.SHA256);
            var key = Convert.ToBase64String(encrypt.GetBytes(keySize));
            var salt = Convert.ToBase64String(encrypt.Salt);

            //Act
            var result = PasswordProtect.CheckPassword($"{iterations}!{salt}!{key}", password);

            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void GetProtectPassword_PasswordToHash_ReturnValidHash()
        {
            //Arrange
            var passwordInit = "qwe123";
            const int KEY_SIZE = 16;

            //Act
            var passwordHash = PasswordProtect.GetPasswordProtect(passwordInit);

            //Arrange
            var partsHash = passwordHash.Split('!');
            var iterationParts = int.Parse(partsHash[0]);
            var saltParts = Convert.FromBase64String(partsHash[1]);
            var keyHashActual = Convert.FromBase64String(partsHash[2]);
            using var encrypt = new Rfc2898DeriveBytes(
                passwordInit,
                saltParts,
                iterationParts,
                HashAlgorithmName.SHA256);

            var keyHashExpected = encrypt.GetBytes(KEY_SIZE);
            keyHashActual.Should().BeEquivalentTo(keyHashExpected);
        }
    }
}