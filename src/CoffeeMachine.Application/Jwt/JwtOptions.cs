using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.IdentityModel.Tokens;

namespace CoffeeMachine.Application.Jwt
{
    public class JwtOptions
    {
        public const string SECRET_KEY = "oGe893nGZl23ieJN1l06";
        public const string ISSUER = "DueviMachine";
        public const int EXPIRATION_TIME = 2;

        public static SymmetricSecurityKey GetSuSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SECRET_KEY)); //почему именно так?
        }
    }
}
