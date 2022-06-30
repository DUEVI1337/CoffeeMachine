﻿using System;
using System.Threading.Tasks;

using CoffeeMachine.Application.Service.Interfaces;
using CoffeeMachine.Domain.Entities;
using CoffeeMachine.Infrastructure;

using Serilog;

namespace CoffeeMachine.Application.Service
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public class PaymentService : IPaymentService
    {
        private readonly UnitOfWork _uow;

        public PaymentService(UnitOfWork uow)
        {
            _uow = uow;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="clientMoney"><inheritdoc/></param>
        /// <param name="coffeeId"><inheritdoc/></param>
        /// <param name="amountDeal"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public async Task AddPaymentAsync(int clientMoney, string coffeeId, int amountDeal)
        {
            _uow.PaymentRepo.Add(new Payment
            {
                PaymentId = Guid.NewGuid(),
                ClientMoney = clientMoney,
                CoffeeId = Guid.Parse(coffeeId),
                Deal = amountDeal
            });
            Log.Information("Payment added");
        }
    }
}