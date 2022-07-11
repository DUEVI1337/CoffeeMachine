using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using CoffeeMachine.Domain.Entities;

using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace CoffeeMachine.Application.Jwt
{
    public class JwtManager
    {
        public string GenerateJwtToken(User user)
        {
            var claims = GetClaims(user);
            var token = new JwtSecurityToken(
                issuer: JwtOptions.ISSUER,
                audience: null,
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(JwtOptions.EXPIRATION_TIME),
                signingCredentials: new(JwtOptions.GetSuSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private List<Claim> GetClaims(User user)
        {
            return new List<Claim>
            {
                new ("UserId", user.IdUser.ToString()),
                new (ClaimsIdentity.DefaultNameClaimType, user.Username),
            };
        }
    }
}
