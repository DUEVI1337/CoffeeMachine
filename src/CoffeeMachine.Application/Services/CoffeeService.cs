﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CoffeeMachine.Application.Exceptions.CustomExceptions;
using CoffeeMachine.Application.Mappers;
using CoffeeMachine.Application.Services.Interfaces;
using CoffeeMachine.Application.Strategy;
using CoffeeMachine.Application.Strategy.Contexts;
using CoffeeMachine.Domain.Dto;
using CoffeeMachine.Domain.Entities;
using CoffeeMachine.Infrastructure;

using Serilog;

namespace CoffeeMachine.Application.Services
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public class CoffeeService : ICoffeeService

    {
        private readonly IBalanceService _balanceService;
        private readonly IBanknoteCashboxService _banknoteCashboxService;

        private readonly Func<int, int, int> _getAmountDeal = (clientMoney, coffeePrice) => clientMoney - coffeePrice;
        private readonly IIncomeService _incomeService;
        private readonly IPaymentService _paymentService;
        private readonly UnitOfWork _uow;
        private DealContext _dealContext;

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
        public async Task<List<BanknoteDto>> BuyCoffeeAsync(CoffeeDto coffee, List<BanknoteDto> clientMoney,
            TypeDeal typeDeal)
        {
            var amountClientMoney = GetAmountClientMoney(clientMoney);
            var cashbox = await _banknoteCashboxService.GetCashboxAsync();
            var amountDeal = _getAmountDeal(amountClientMoney, coffee.CoffeePrice);

            if (!СheckPossibleGiveDeal(amountDeal, cashbox))
                return null;

            clientMoney.ForEach(x =>
                cashbox.FirstOrDefault(y => y.Denomination == x.Denomination)!.CountBanknote += x.CountBanknote);
            var (deal, updatedCashbox) = ExecuteStrategy(typeDeal, cashbox, amountDeal);
            await AddChangesInDbAsync(coffee, updatedCashbox, amountClientMoney);
            return deal;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="id">id coffee</param>
        /// <returns><inheritdoc/></returns>
        public async Task<CoffeeDto> GetCoffeeDtoByIdAsync(string id)
        {
            if (!Guid.TryParse(id, out var idGuid))
                throw new NullReferenceException();

            var coffee = await _uow.CoffeeRepo.GetByIdAsync(idGuid);
            return coffee == null ? throw new NullReferenceException() : Mapper.MapToCoffeeDto(coffee);
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
        /// Add and save changes after buying coffee in database
        /// </summary>
        /// <param name="coffee">coffee from order of client</param>
        /// <param name="updatedCashbox">updated cashbox of coffee machine</param>
        /// <param name="clientMoney">money of client</param>
        /// <returns></returns>
        private async Task AddChangesInDbAsync(CoffeeDto coffee, List<BanknoteCashbox> updatedCashbox, int clientMoney)
        {
            var amountDeal = _getAmountDeal(clientMoney, coffee.CoffeePrice);

            await _banknoteCashboxService.UpdateCashboxAsync(updatedCashbox);
            _paymentService.AddPayment(clientMoney, coffee.CoffeeId, amountDeal);
            await _incomeService.AddIncomeAsync(coffee.CoffeePrice);
            await _balanceService.UpdateBalanceAsync(coffee.CoffeeId, coffee.CoffeePrice);
            await _uow.SaveChangesAsync();
            Log.Information("New data updated in database");
        }

        /// <summary>
        /// Executing strategy that give deal
        /// </summary>
        /// <param name="typeDeal">type of deal</param>
        /// <param name="cashbox">cashbox of coffee machine</param>
        /// <param name="amountDeal">amount money that need give client</param>
        /// <returns><see cref="List{T}"/> where T <see cref="BanknoteDto"/> and <see cref="List{T}"/> T2 <see cref="BanknoteCashbox"/> (Tuple)</returns>
        /// <exception cref="NullCashboxException">impossible give deal</exception>
        private (List<BanknoteDto>, List<BanknoteCashbox>) ExecuteStrategy(TypeDeal typeDeal,
            List<BanknoteCashbox> cashbox, int amountDeal)
        {
            _dealContext = new DealContext(DealFactory.GetDealStrategy(typeDeal.ToString()));
            var (deal, updatedCashbox) = _dealContext.GiveDeal(GetCopyCashbox(cashbox), amountDeal);
            while (deal == null)
            {
                var newStrategy = DealFactory.GetNextDealStrategy(typeDeal.ToString());
                if (newStrategy == null)
                    throw new NullCashboxException();

                typeDeal = Enum.Parse<TypeDeal>(newStrategy.Value.Key);
                _dealContext = new DealContext(newStrategy.Value.Value);
                (deal, updatedCashbox) = _dealContext.GiveDeal(GetCopyCashbox(cashbox), amountDeal);
            }

            return (deal, updatedCashbox);
        }

        /// <summary>
        /// Calculating amount money that client was injected into the coffee machine
        /// </summary>
        /// <param name="banknotes">banknote of client</param>
        /// <returns><see cref="int"/>, amount money of client</returns>
        private static int GetAmountClientMoney(List<BanknoteDto> banknotes)
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
        /// Copying <see cref="List{T}"/> to new <see cref="List{T}"/> for keep to original list
        /// </summary>
        /// <param name="cashbox">cashbox of coffee machine</param>
        /// <returns><see cref="List{T}"/> where T <see cref="BanknoteCashbox"/></returns>
        private List<BanknoteCashbox> GetCopyCashbox(List<BanknoteCashbox> cashbox)
        {
            return cashbox.Select(banknote => new BanknoteCashbox
            {
                BanknoteId = banknote.BanknoteId,
                CountBanknote = banknote.CountBanknote,
                Denomination = banknote.Denomination
            }).ToList();
        }

        /// <summary>
        /// Checking possible give deal to client
        /// </summary>
        /// <param name="amountDeal">amount money that need give client</param>
        /// <param name="cashbox">cashbox of coffee machine</param>
        /// <returns><see cref="bool"/>, 'true' if possible give deal, 'false' deal is zero</returns>
        /// <exception cref="NotEnoughMoneyException">not enough client of money for buying coffee</exception>
        /// <exception cref="NullCashboxException">In cashbox of coffee machine not enough money</exception>
        private bool СheckPossibleGiveDeal(int amountDeal, List<BanknoteCashbox> cashbox)
        {
            if (amountDeal < 0)
                throw new NotEnoughMoneyException();

            if (!cashbox.Select(x => x.CountBanknote != 0).Any())
                throw new NullCashboxException();

            return amountDeal != 0;
        }
    }
}