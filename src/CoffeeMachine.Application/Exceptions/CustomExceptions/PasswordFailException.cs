using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMachine.Application.Exceptions.CustomExceptions
{
    public class PasswordFailException : Exception
    {
        public PasswordFailException()
        {
        }

        public PasswordFailException(string message)
            : base(message)
        {
        }

        public PasswordFailException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
