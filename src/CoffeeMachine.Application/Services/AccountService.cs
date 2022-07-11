using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CoffeeMachine.Application.Dto;
using CoffeeMachine.Application.Exceptions.CustomExceptions;
using CoffeeMachine.Application.Jwt;
using CoffeeMachine.Application.Services.Interfaces;
using CoffeeMachine.Domain.Entities;
using CoffeeMachine.Infrastructure;

namespace CoffeeMachine.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly UnitOfWork _uow;
        private readonly JwtManager _jwtManager;

        public AccountService(UnitOfWork uow, JwtManager jwtManager)
        {
            _uow = uow;
            _jwtManager = jwtManager;
        }

        public async Task RegisterAccountAsync(RegisterDto registerDto)
        {
            if (!await CheckUsernameUniqueAsync(registerDto.Username))
                throw new UsernameNotUniqueException();

            User user = new()
            {
                IdUser = Guid.NewGuid(),
                Username = registerDto.Username,
                Password = PasswordProtect.GetPasswordProtect(registerDto.Password)
            };
            _uow.UserRepo.Add(user);
            await _uow.SaveChangesAsync();
        }

        public async Task<string> SignInAccountAsync(SignInDto signInDto)
        {
            User user = await _uow.UserRepo.FindAsync(x=>x.Username == signInDto.Username);
            if (user == null)
                throw new SignInFailException();

            var result = PasswordProtect.CheckPassword(user.Password, signInDto.Password);
            if (!result)
                throw new SignInFailException();
            return _jwtManager.GenerateJwtToken(user);
        }

        /// <summary>
        /// Сhecks the user name in the database for uniqueness
        /// </summary>
        /// <param name="username">username of user</param>
        /// <returns>true or false</returns>
        private async Task<bool> CheckUsernameUniqueAsync(string username)
        {
            User user = await _uow.UserRepo.FindAsync(x => x.Username == username);
            return user == null;
        }
    }
}
