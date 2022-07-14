using System.Text;

using Microsoft.IdentityModel.Tokens;

namespace CoffeeMachine.Application.Jwt
{
    /// <summary>
    /// options to create jwt-token
    /// </summary>
    public class JwtOptions
    {
        public const int EXPIRATION_TIME = 30; //token lifetime
        public const string ISSUER = "DueviMachine"; //token issuer
        public const string SECRET_KEY = "oGe893nGZl23ieJN1l06"; //token secret key

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