﻿using System;
using System.Collections.Generic;

using CoffeeMachine.Domain.Dto;

namespace CoffeeMachine.Web.Exceptions.CustomExceptions
{
    /// <summary>
    /// Sent to client when impossible to give deal 
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
    }
}
