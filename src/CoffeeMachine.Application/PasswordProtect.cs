using System;
using System.Linq;
using System.Security.Cryptography;

namespace CoffeeMachine.Application
{
    /// <summary>
    /// For work with password of user
    /// </summary>
    public class PasswordProtect
    {
        private const int ITERATIONS = 1000; //number iterations for algorithm 'RFC 2898'
        private const int KEY_SIZE = 16; //key size of password hash 
        private const int SALT_SIZE = 8; //salt size of password hash
        private static readonly HashAlgorithmName _algorithm = HashAlgorithmName.SHA256; //hashing algorithm

        /// <summary>
        /// Checking password hash from db and password from dto
        /// </summary>
        /// <param name="passwordHash">user password hash from db</param>
        /// <param name="password">password from dto</param>
        /// <returns></returns>
        public static bool CheckPassword(string passwordHash, string password)
        {
            var partsPasswordHash = passwordHash.Split('!');
            var iterationsPart = int.Parse(partsPasswordHash[0]);
            var saltPart = Convert.FromBase64String(partsPasswordHash[1]);
            var keyPart = Convert.FromBase64String(partsPasswordHash[2]);

            using var encrypt = new Rfc2898DeriveBytes(
                password,
                saltPart,
                iterationsPart,
                _algorithm);

            var keyPassword = encrypt.GetBytes(KEY_SIZE);

            return keyPart.SequenceEqual(keyPassword);
        }

        /// <summary>
        /// Create password hash
        /// </summary>
        /// <param name="password">user password</param>
        /// <returns></returns>
        public static string GetPasswordProtect(string password)
        {
            using var encrypt = new Rfc2898DeriveBytes(
                password,
                SALT_SIZE,
                ITERATIONS,
                _algorithm);

            var key = Convert.ToBase64String(encrypt.GetBytes(KEY_SIZE));
            var salt = Convert.ToBase64String(encrypt.Salt);
            return $"{ITERATIONS}!{salt}!{key}";
        }
    }
}