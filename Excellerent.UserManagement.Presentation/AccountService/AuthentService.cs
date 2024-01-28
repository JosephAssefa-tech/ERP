using Excellerent.Usermanagement.Domain.Entities;
using Excellerent.UserManagement.Presentation.Helper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BC = BCrypt.Net.BCrypt;

namespace Excellerent.UserManagement.Presentation.AccoutService
{
    public class AuthentService : IAuthentService
    {
        private readonly IConfigurationSection _jwtConfig;

        public AuthentService(IConfigurationSection jwtConfig)
        {
            _jwtConfig = jwtConfig;
        }

 

        public string Authenticate(UserEntity userEntity)
        {
            if (userEntity == null)
                return null;
            SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig["SecretKey"]));
            var credentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
            audience: _jwtConfig["Audience"],
            issuer: _jwtConfig["Issuer"],
            claims: new List<Claim>
            {
                new Claim("Email", userEntity.Email)
            },
            expires: DateTime.UtcNow.AddMinutes(360),
            signingCredentials: credentials
        );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return tokenString;
        }

        public string HashPassword(string password)
        {
            return BC.HashPassword(password);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            // return BC.Verify(password, hashedPassword);
            return PasswordCryptographyPbkdf2.VerifyPassword(hashedPassword, password);

        }
    }
}
