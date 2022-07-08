using System.Threading.Tasks;

using CoffeeMachine.Application.Dto;

namespace CoffeeMachine.Application.Services.Interfaces
{
    public interface IAccountService
    {
        Task RegisterAccountAsync(RegisterDto registerDto);
    }
}