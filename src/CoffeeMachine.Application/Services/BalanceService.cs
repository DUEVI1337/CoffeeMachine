using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CoffeeMachine.Application.Dto;
using CoffeeMachine.Application.Mappers;
using CoffeeMachine.Application.Services.Interfaces;
using CoffeeMachine.Domain.Entities;
using CoffeeMachine.Infrastructure;

using Serilog;

namespace CoffeeMachine.Application.Services
{
    public class BalanceService : IBalanceService
    {
        private readonly UnitOfWork _uow;

        public BalanceService(UnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<List<BalanceDto>> GetBalancesDtoAsync()
        {
            var balances = await _uow.BalanceRepo.GetAllAsync();
            return balances.Select(x => Mapper.MapToBalanceDto(x)).ToList();
        }

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