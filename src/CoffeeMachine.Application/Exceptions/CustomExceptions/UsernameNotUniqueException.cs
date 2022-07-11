using System;

namespace CoffeeMachine.Application.Exceptions.CustomExceptions
{
    public class UsernameNotUniqueException : Exception
    {
        public UsernameNotUniqueException()
        {
        }

        public UsernameNotUniqueException(string message)
            : base(message)
        {
        }

        public UsernameNotUniqueException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}