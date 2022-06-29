using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CoffeeMachine.Application.Mappers;
using CoffeeMachine.Application.Service.Interfaces;
using CoffeeMachine.Application.Strategy;
using CoffeeMachine.Application.Strategy.Contexts;
using CoffeeMachine.Domain.Dto;
using CoffeeMachine.Domain.DTO;
using CoffeeMachine.Domain.Entities;
using CoffeeMachine.Infrastructure;

namespace CoffeeMachine.Application.Service
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public class CoffeeService : ICoffeeService

    {
        private readonly UnitOfWork _uow;
        private DealContext _dealContext;

        public CoffeeService(UnitOfWork uow)
        {
            _uow = uow;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="coffeePrice"><inheritdoc/></param>
        /// <param name="clientMoney"><inheritdoc/></param>
        /// <param name="typeDeal"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public List<BanknoteDto> BuyCoffee(int coffeePrice, int clientMoney, List<BanknoteCashBox> cashbox,
            TypeDeal typeDeal)
        {
            if (clientMoney < coffeePrice)
                return null;

            List<BanknoteDto> deal = new();
            var amountDeal = clientMoney - coffeePrice;
            if (amountDeal == 0)
                return deal;

            _dealContext = new DealContext(DealFactory.GetDealStrategy(typeDeal.ToString()));
            deal = _dealContext.GiveDeal(GetCopyCashbox(cashbox), amountDeal);
            while (deal == null)
            {
                var newStrategy = DealFactory.GetNextDealStrategy(typeDeal.ToString());
                if (newStrategy == null)
                    return null;

                typeDeal = Enum.Parse<TypeDeal>(newStrategy.Value.Key);
                _dealContext = new DealContext(newStrategy.Value.Value);
                deal = _dealContext.GiveDeal(GetCopyCashbox(cashbox), amountDeal);
            }

            return deal;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="banknotes"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public int GetAmountClientMoney(List<BanknoteDto> banknotes)
        {
            var clientMoney = 0; //money person input in coffee machine
            foreach (var banknote in banknotes)
            {
                for (var j = 0; j < banknote.CountBanknote; j++)
                    clientMoney += banknote.Denomination;
            }

            return clientMoney;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="id">id coffee</param>
        /// <returns><inheritdoc/></returns>
        public async Task<CoffeeDto> GetCoffeeDtoByIdAsync(string id)
        {
            if (!Guid.TryParse(id, out var idGuid))
                return null;

            var coffee = await _uow.CoffeeRepo.GetByIdAsync(idGuid);
            return coffee == null ? null : Mapper.MapToCoffeeDto(coffee);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns><inheritdoc/></returns>
        public async Task<List<CoffeeDto>> GetListCoffeeDtoAsync()
        {
            var coffees = await _uow.CoffeeRepo.GetAllAsync();
            return coffees.Select(x => Mapper.MapToCoffeeDto(x)).ToList();
        }

        /// <summary>
        /// Copying <see cref="List{T}"/> to new <see cref="List{T}"/> for keep to original list
        /// </summary>
        /// <param name="cashbox">cashbox of coffee machine</param>
        /// <returns><see cref="List{T}"/> where T <see cref="BanknoteCashBox"/></returns>
        private List<BanknoteCashBox> GetCopyCashbox(List<BanknoteCashBox> cashbox)
        {
            var copyCashbox = cashbox.Select(banknote => new BanknoteCashBox
            {
                CountBanknote = banknote.CountBanknote,
                Denomination = banknote.Denomination
            }).ToList();
            return copyCashbox;
        }
    }
}