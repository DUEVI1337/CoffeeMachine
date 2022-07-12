﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using CoffeeMachine.Domain.Entities;

using Microsoft.IdentityModel.Tokens;

namespace CoffeeMachine.Application.Jwt
{
    /// <summary>
    /// Work with JWT-tokens
    /// </summary>
    public class JwtManager
    {
        /// <summary>
        /// Generate jwt-token for user
        /// </summary>
        /// <param name="user">user for which you want create jwt-token</param>
        /// <returns>jwt-token</returns>
        public string GenerateJwtToken(User user)
        {
            var claims = GetClaims(user);
            var token = new JwtSecurityToken(
                JwtOptions.ISSUER,
                null,
                claims,
                DateTime.Now,
                DateTime.Now.AddMinutes(JwtOptions.EXPIRATION_TIME),
                new(JwtOptions.GetSuSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Gets claims for jwt-token
        /// </summary>
        /// <param name="user">user for which you want to create claims</param>
        /// <returns><see cref="List{T}"/> where T <see cref="Claim"/></returns>
        private List<Claim> GetClaims(User user)
        {
            return new List<Claim>
            {
                new("UserId", user.IdUser.ToString()),
                new(ClaimsIdentity.DefaultNameClaimType, user.Username),
            };
        }
    }
}