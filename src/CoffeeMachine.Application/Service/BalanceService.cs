using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CoffeeMachine.Application.Mappers;
using CoffeeMachine.Application.Service.Interfaces;
using CoffeeMachine.Domain.Dto;
using CoffeeMachine.Infrastructure;

namespace CoffeeMachine.Application.Service
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public class BalanceService : IBalanceService
    {
        private readonly UnitOfWork _uow;

        public BalanceService(UnitOfWork uow)
        {
            _uow = uow;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns><inheritdoc/></returns>
        public async Task<List<BalanceDto>> GetBalancesDtoAsync()
        {
            var balances = await _uow.BalanceRepo.GetAllAsync();
            return balances.Select(x => Mapper.MapToBalanceDto(x)).ToList();
        }
    }
}