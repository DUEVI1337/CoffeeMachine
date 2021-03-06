using System;
using System.Linq;
using System.Threading.Tasks;

using CoffeeMachine.Application.Services.Interfaces;
using CoffeeMachine.Domain.Entities;
using CoffeeMachine.Infrastructure;

using Serilog;

namespace CoffeeMachine.Application.Services
{
    /// <summary>
    /// Work with <see cref="Income"/> entity from 'Infrastructure' layer
    /// </summary>
    public class IncomeService : IIncomeService
    {
        private readonly UnitOfWork _uow;

        public IncomeService(UnitOfWork uow)
        {
            _uow = uow;
        }

        ///<inheritdoc/>
        public async Task AddIncomeAsync(int coffeePrice)
        {
            var income = await _uow.IncomeRepo.GetAllAsync();
            var todayIncome = income.FirstOrDefault(x => x.Date.Day == DateTime.UtcNow.Day);
            if (todayIncome == null)
            {
                _uow.IncomeRepo.Add(new Income
                {
                    Date = DateTime.UtcNow,
                    IncomeId = Guid.NewGuid(),
                    TotalIncome = coffeePrice
                });
                Log.Information("Income added");
            }
            else
            {
                todayIncome.TotalIncome += coffeePrice;
                _uow.IncomeRepo.Update(todayIncome);
                Log.Information("Income updated");
            }
        }
    }
}