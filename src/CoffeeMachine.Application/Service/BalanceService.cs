using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CoffeeMachine.Application.Mappers;
using CoffeeMachine.Application.Service.Interfaces;
using CoffeeMachine.Domain.Dto;
using CoffeeMachine.Domain.Entities;
using CoffeeMachine.Infrastructure;

using Serilog;

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

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="coffeeId"><inheritdoc/></param>
        /// <param name="coffeePrice"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public async Task UpdateBalanceAsync(string coffeeId, int coffeePrice)
        {
            var balances = await _uow.BalanceRepo.GetAllAsync();
            var balanceCoffee = balances.FirstOrDefault(x => x.CoffeeId == Guid.Parse(coffeeId));
            if (balanceCoffee == null)
            {
                _uow.BalanceRepo.Add(new Balance
                {
                    BalanceId = Guid.NewGuid(),
                    EarnedMoney = coffeePrice,
                    CoffeeId = Guid.Parse(coffeeId)
                });
                Log.Information("New balance add in database");
            }
            else
            {
                balanceCoffee.EarnedMoney += coffeePrice;
                _uow.BalanceRepo.Update(balanceCoffee);
                Log.Information("Balance in database update");
            }
        }
    }
}