﻿using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Security
{
    public class TokenBuilder : ITokenBuilder
    {
        public string Build(string name, string[] roles, DateTime expireDate)
        {
            var handler = new JwtSecurityTokenHandler();

            var claims = new List<Claim>();

            foreach (var userRole in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity(name, "Bearer"),
                claims
            );

            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = TokenAuthOption.Issuer,
                Audience = TokenAuthOption.Audience,
                SigningCredentials = TokenAuthOption.SigningCredentials,
                Subject = identity,
                Expires = expireDate
            });

            return handler.WriteToken(securityToken);
        }
    }   
}
