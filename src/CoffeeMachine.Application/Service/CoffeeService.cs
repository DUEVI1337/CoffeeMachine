﻿using System;
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
        private readonly IBalanceService _balanceService;
        private readonly IPaymentService _paymentService;
        private readonly IBanknoteCashboxService _banknoteCashboxService;
        private readonly IIncomeService _incomeService;

        public CoffeeService(UnitOfWork uow, IBanknoteCashboxService banknoteCashboxService,
            IBalanceService balanceService, IPaymentService paymentService, IIncomeService incomeService)
        {
            _uow = uow;
            _banknoteCashboxService = banknoteCashboxService;
            _incomeService = incomeService;
            _paymentService = paymentService;
            _balanceService = balanceService;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="coffee"><inheritdoc/></param>
        /// <param name="clientMoney"><inheritdoc/></param>
        /// <param name="typeDeal"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public async Task<List<BanknoteDto>> BuyCoffee(CoffeeDto coffee, List<BanknoteDto> clientMoney, TypeDeal typeDeal)
        {
            int amountClientMoney = GetAmountClientMoney(clientMoney);
            var cashbox = await _banknoteCashboxService.GetCashboxAsync();
            var amountDeal = amountClientMoney - coffee.CoffeePrice;

            if (amountDeal < 0 || !cashbox.Select(x=>x.CountBanknote != 0).Any())
                return null;

            List<BanknoteDto> deal = new();
            List<BanknoteCashbox> updatedCashbox = new();

            if (amountDeal == 0)
                return deal;

            clientMoney.ForEach(x =>
                cashbox.FirstOrDefault(y => y.Denomination == x.Denomination)!.CountBanknote += x.CountBanknote);
            _dealContext = new DealContext(DealFactory.GetDealStrategy(typeDeal.ToString()));
            (deal, updatedCashbox) = _dealContext.GiveDeal(GetCopyCashbox(cashbox), amountDeal);
            while (deal == null)
            {
                var newStrategy = DealFactory.GetNextDealStrategy(typeDeal.ToString());
                if (newStrategy == null)
                    return null;

                typeDeal = Enum.Parse<TypeDeal>(newStrategy.Value.Key);
                _dealContext = new DealContext(newStrategy.Value.Value);
                (deal, updatedCashbox) = _dealContext.GiveDeal(GetCopyCashbox(cashbox), amountDeal);
            }

            await _banknoteCashboxService.UpdateCashbox(updatedCashbox);
            await _paymentService.AddPayment(amountClientMoney, coffee.CoffeeId, amountDeal);
            await _incomeService.AddIncome(coffee.CoffeePrice);
            await _balanceService.UpdateBalance(coffee.CoffeeId, coffee.CoffeePrice);
            await _uow.SaveChangesAsync();

            return deal;
        }

        /// <summary>
        /// Calculating amount money that client was injected into the coffee machine
        /// </summary>
        /// <param name="banknotes">banknote of client</param>
        /// <returns><see cref="int"/>, amount money of client</returns>
        private int GetAmountClientMoney(List<BanknoteDto> banknotes)
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
        /// <returns><see cref="List{T}"/> where T <see cref="BanknoteCashbox"/></returns>
        private static List<BanknoteCashbox> GetCopyCashbox(List<BanknoteCashbox> cashbox)
        {
            var copyCashbox = cashbox.Select(banknote => new BanknoteCashbox
            {
                BanknoteId = banknote.BanknoteId,
                CountBanknote = banknote.CountBanknote,
                Denomination = banknote.Denomination
            }).ToList();
            return copyCashbox;
        }
    }
}