using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.IdentityModel.Tokens;

namespace CoffeeMachine.Application.Jwt
{
    /// <summary>
    /// options to create jwt-token
    /// </summary>
    public class JwtOptions
    {
        public const string SECRET_KEY = "oGe893nGZl23ieJN1l06"; //token secret key
        public const string ISSUER = "DueviMachine"; //token issuer
        public const int EXPIRATION_TIME = 30; //token lifetime

        /// <summary>
        /// get 'SECRET_KEY' as on array of bytes (ASCII encoding)
        /// </summary>
        /// <returns></returns>
        public static SymmetricSecurityKey GetSuSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SECRET_KEY));
        }
    }
}
