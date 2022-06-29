using System.Collections.Generic;
using System.Threading.Tasks;

using CoffeeMachine.Application.Service.Interfaces;
using CoffeeMachine.Domain.Entities;
using CoffeeMachine.Infrastructure;

namespace CoffeeMachine.Application.Service
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public class BanknoteCashboxService : IBanknoteCashboxService
    {
        private readonly UnitOfWork _uow;

        public BanknoteCashboxService(UnitOfWork uow)
        {
            _uow = uow;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public async Task<List<BanknoteCashBox>> GetCashboxAsync()
        {
            return await _uow.BanknoteCashboxRepo.GetAllAsync();
        }
    }
}