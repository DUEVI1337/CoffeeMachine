using System;

namespace CoffeeMachine.Application.Exceptions.CustomExceptions
{
    /// <summary>
    /// Sent to client when not enough money of client for buying coffee
    /// </summary>
    public class NotEnoughMoneyException : Exception
    {
        public NotEnoughMoneyException()
        {
        }

        public NotEnoughMoneyException(string message)
            : base(message)
        {
        }

        public NotEnoughMoneyException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}