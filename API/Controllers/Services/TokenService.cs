using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System;

namespace API.Controllers.Services
{
    public class TokenService : ITokenService
    {

        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration config)
        {            
            _key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(config["TokenKey"]));
        }

        string ITokenService.CreateToken(AppUser user)
        {
            var claims = new List<Claim> {
              new Claim(JwtRegisteredClaimNames.NameId, user.UserName)
            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.EcdsaSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}