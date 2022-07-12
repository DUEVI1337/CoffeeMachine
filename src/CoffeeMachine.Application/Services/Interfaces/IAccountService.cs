using System.Threading.Tasks;

using CoffeeMachine.Application.Dto;
using CoffeeMachine.Application.Exceptions.CustomExceptions;

namespace CoffeeMachine.Application.Services.Interfaces
{
    /// <summary>
    /// Work with user account
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// register new user into app
        /// </summary>
        /// <param name="registerDto">data for register</param>
        /// <exception cref="UsernameNotUniqueException">Username from dto not unique in db</exception>
        Task RegisterAccountAsync(RegisterDto registerDto);

        /// <summary>
        /// authorize user into app
        /// </summary>
        /// <param name="signInDto">data for authorize</param>
        /// <returns>jwt-token</returns>
        /// <exception cref="SignInFailException">password or username from dto invalid</exception>
        Task<string> SignInAccountAsync(SignInDto signInDto);
    }
}