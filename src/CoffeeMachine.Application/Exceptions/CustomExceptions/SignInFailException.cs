using System;

namespace CoffeeMachine.Application.Exceptions.CustomExceptions
{
    public class SignInFailException : Exception
    {
        public SignInFailException()
        {
        }

        public SignInFailException(string message)
            : base(message)
        {
        }

        public SignInFailException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}