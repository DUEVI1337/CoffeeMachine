using System.Threading.Tasks;

using CoffeeMachine.Application.Dto;
using CoffeeMachine.Application.Services.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeMachine.Web.Controllers
{
    /// <summary>
    /// work with user account
    /// </summary>
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
        /// register new user into app
        /// </summary>
        /// <param name="registerDto">data for register</param>
        [HttpPost]
        [Route("Register")]
        public async Task RegisterAccount([FromBody] RegisterDto registerDto)
        {
            await _accountService.RegisterAccountAsync(registerDto);
        }

        /// <summary>
        /// authorize user into app
        /// </summary>
        /// <param name="signInDto">data for authorize</param>
        /// <returns>jwt-token</returns>
        [HttpPost]
        [Route("SignIn")]
        public async Task<string> SignInAccount([FromBody] SignInDto signInDto)
        {
            return await _accountService.SignInAccountAsync(signInDto);
        }
    }
}