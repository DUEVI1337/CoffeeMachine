using System;
using System.Collections.Generic;

using CoffeeMachine.Application.Dto;

namespace CoffeeMachine.Application.Exceptions.CustomExceptions
{
    /// <summary>
    /// Sent to client when not enough money in coffee machine cashbox
    /// </summary>
    public class NullCashboxException : Exception
    {
        public NullCashboxException()
        {
        }

        public NullCashboxException(string message)
            : base(message)
        {
        }

        public NullCashboxException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public List<BanknoteDto> ClientMoney { get; set; }
    }
}