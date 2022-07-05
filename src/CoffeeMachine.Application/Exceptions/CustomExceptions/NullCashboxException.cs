﻿using System;
using System.Collections.Generic;

using CoffeeMachine.Domain.Dto;

namespace CoffeeMachine.Application.Exceptions.CustomExceptions
{
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