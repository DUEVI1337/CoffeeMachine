using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CoffeeMachine.Application.Services.Interfaces;
using CoffeeMachine.Domain.Entities;
using CoffeeMachine.Infrastructure;

using Serilog;

namespace CoffeeMachine.Application.Services
{
    public class BanknoteCashboxService : IBanknoteCashboxService
    {
        private readonly UnitOfWork _uow;

        public BanknoteCashboxService(UnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<List<BanknoteCashbox>> GetCashboxAsync()
        {
            return await _uow.BanknoteCashboxRepo.GetAllAsync();
        }

        public async Task UpdateCashboxAsync(List<BanknoteCashbox> updatedCashbox)
        {
            var cashbox = await _uow.BanknoteCashboxRepo.GetAllAsync();
            foreach (var banknote in updatedCashbox)
            {
                cashbox.FirstOrDefault(x => x.BanknoteId == banknote.BanknoteId)!.CountBanknote =
                    banknote.CountBanknote;
            }

            _uow.BanknoteCashboxRepo.UpdateRange(cashbox);
            Log.Information("Cashbox of coffee machine updated");
        }
    }
}