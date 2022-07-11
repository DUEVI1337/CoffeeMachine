using System;
using System.Security.Cryptography;
using System.Threading.Tasks;

using CoffeeMachine.Application.Dto;
using CoffeeMachine.Application.Exceptions.CustomExceptions;
using CoffeeMachine.Application.Services;
using CoffeeMachine.Application.Services.Interfaces;
using CoffeeMachine.Domain.Entities;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeMachine.Web.Controllers
{
    [Route("account/v1")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="registerDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Register")]
        public async Task RegisterAccount([FromBody] RegisterDto registerDto)
        {
            if (ModelState.IsValid)
            {
                await _accountService.RegisterAccountAsync(registerDto);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="signInDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SignIn")]
        public async Task<string> SignInAccount([FromBody] SignInDto signInDto)
        {
            if (ModelState.IsValid)
            {
                return await _accountService.SignInAccountAsync(signInDto);
            }

            return null;
        }
    }
}
