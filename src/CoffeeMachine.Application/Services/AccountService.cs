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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="registerDto"></param>
        /// <returns></returns>
        /// <exception cref="PasswordFailException"></exception>
        /// <exception cref="UsernameNotUniqueException"></exception>
        public async Task RegisterAccountAsync(RegisterDto registerDto)
        {
            if (string.IsNullOrWhiteSpace(registerDto.Password))
                throw new PasswordFailException();

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="signInDto"></param>
        /// <returns></returns>
        /// <exception cref="SignInFailException"></exception>
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
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        private async Task<bool> CheckUsernameUniqueAsync(string username)
        {
            User user = await _uow.UserRepo.FindAsync(x => x.Username == username);
            return user == null;
        }
    }
}
