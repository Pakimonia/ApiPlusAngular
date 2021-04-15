using DataAccess.EF;
using DataAccess.Entitty;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Project.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Implementations
{
    public class JWTTokenService : IJWTTokenService
    {
        private readonly EFContext _context;

        private readonly IConfiguration _configuration;

        private readonly UserManager<User> _userManager;

        public JWTTokenService(EFContext context, IConfiguration configuration, UserManager<User> userManager)
        {
            this._context = context;
            this._configuration = configuration;
            this._userManager = userManager;
        }

        public string CreateToken(User user)
        {
            var roles = _userManager.GetRolesAsync(user).Result;

            var claims = new List<Claim>
            {
                new Claim("id", user.Id),
                new Claim("email", user.Email)
            };

            claims.AddRange(roles.Select(r => new Claim("roles", r)));

            string jwtTokenSecretKey = _configuration["SecretPhrase"];

            var signInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtTokenSecretKey));
            var signInCredentians = new SigningCredentials(signInKey, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(
                signingCredentials: signInCredentians,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(14)
                );

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}
