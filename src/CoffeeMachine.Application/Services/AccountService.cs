using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CoffeeMachine.Application.Dto;
using CoffeeMachine.Application.Services.Interfaces;
using CoffeeMachine.Domain.Entities;
using CoffeeMachine.Infrastructure;

namespace CoffeeMachine.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly UnitOfWork _uow;

        public AccountService(UnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task RegisterAccountAsync(RegisterDto registerDto)
        {
            if (string.IsNullOrWhiteSpace(registerDto.Password))
                throw new Exception();

            User user = new() { IdUser = Guid.NewGuid(), Username = registerDto.Username };
            user.Password = GetProtectPassword(registerDto.Password);
            var result = ChekPassword(user.Password, "string");
            _uow.UserRepo.Add(user);
            await _uow.SaveChangesAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        private string GetProtectPassword(string password)
        {
            const int ITERATIONS = 1000;
            const int SALT_SIZE = 8;
            const int KEY_SIZE = 16;
            HashAlgorithmName algorithm = HashAlgorithmName.SHA256;

            using var encrypt = new Rfc2898DeriveBytes(
                password,
                SALT_SIZE,
                ITERATIONS,
                algorithm);

            var key = Convert.ToBase64String(encrypt.GetBytes(KEY_SIZE));
            var salt = Convert.ToBase64String(encrypt.Salt);
            return $"{ITERATIONS}+{salt}+{key}";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="passwordHash"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private bool ChekPassword(string passwordHash, string password)
        {
            var result = false;
            const int ITERATIONS = 1000;
            const int SALT_SIZE = 8;
            const int KEY_SIZE = 16;
            HashAlgorithmName algorithm = HashAlgorithmName.SHA256;

            using var encrypt = new Rfc2898DeriveBytes(
                password,
                SALT_SIZE,
                ITERATIONS,
                algorithm);

            if(passwordHash == password)
                return true;

            return false;
        }
    }
}
