using System;

using CoffeeMachine.Application.Services.Interfaces;
using CoffeeMachine.Domain.Entities;
using CoffeeMachine.Infrastructure;

using Serilog;

namespace CoffeeMachine.Application.Services
{
    /// <summary>
    /// Work with <see cref="Payment"/> entity from 'Infrastructure' layer
    /// </summary>
    public class PaymentService : IPaymentService
    {
        private readonly UnitOfWork _uow;

        public PaymentService(UnitOfWork uow)
        {
            _uow = uow;
        }

        ///<inheritdoc/>
        public void AddPayment(int clientMoney, string coffeeId, int amountDeal)
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