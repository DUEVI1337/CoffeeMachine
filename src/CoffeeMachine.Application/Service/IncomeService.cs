using System;
using System.Linq;
using System.Threading.Tasks;

using CoffeeMachine.Application.Service.Interfaces;
using CoffeeMachine.Domain.Entities;
using CoffeeMachine.Infrastructure;

namespace CoffeeMachine.Application.Service
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public class IncomeService : IIncomeService
    {
        private readonly UnitOfWork _uow;

        public IncomeService(UnitOfWork uow)
        {
            _uow = uow;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="coffeePrice"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public async Task AddIncome(int coffeePrice)
        {
            var income = await _uow.IncomeRepo.GetAllAsync();
            var todayIncome = income.FirstOrDefault(x => x.Date == DateTime.UtcNow);
            if (todayIncome == null)
            {
                _uow.IncomeRepo.Add(new Income
                {
                    Date = DateTime.UtcNow,
                    IncomeId = Guid.NewGuid(),
                    TotalIncome = coffeePrice
                });
            }
            else
            {
                todayIncome.TotalIncome += coffeePrice;
                _uow.IncomeRepo.Update(todayIncome);
            }
        }
    }
}