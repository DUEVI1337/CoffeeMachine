using System.Threading.Tasks;

using CoffeeMachine.Application.Dto;

namespace CoffeeMachine.Application.Services.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="registerDto"></param>
        /// <returns></returns>
        Task RegisterAccountAsync(RegisterDto registerDto);
        /// <summary>
        /// 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        Task<string> SignInAccountAsync(SignInDto signInDto);
    }
}