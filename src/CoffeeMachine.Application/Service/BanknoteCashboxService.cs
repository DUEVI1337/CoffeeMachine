using System.Collections.Generic;
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
        public async Task<List<BanknoteCashbox>> GetCashboxAsync()
        {
            return await _uow.BanknoteCashboxRepo.GetAllAsync();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="updatedCashbox"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public async Task UpdateCashbox(List<BanknoteCashbox> updatedCashbox)
        {
            var cashbox = await _uow.BanknoteCashboxRepo.GetAllAsync();
            foreach (var banknote in updatedCashbox)
            {
                if (cashbox.Select(x => x.BanknoteId).Contains(banknote.BanknoteId))
                {
                    cashbox.FirstOrDefault(x => x.BanknoteId == banknote.BanknoteId).CountBanknote =
                        banknote.CountBanknote;
                }
            }
            _uow.BanknoteCashboxRepo.UpdateRange(cashbox);
        }
    }
}