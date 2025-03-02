using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SignalRPractice.Service
{
    public class TokenService
    {
        private readonly IConfiguration _config;

        public TokenService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(IdentityUser user, IList<string> roles)
        {

            List<Claim> claims = new()
            {
                new(ClaimTypes.NameIdentifier,user.Id),
                new(ClaimTypes.Name,user.UserName),
                new(ClaimTypes.Email,user.Email),
            };

            foreach (var item in roles)
            {
                claims.Add(new(ClaimTypes.Role, item));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            var token = new JwtSecurityToken(claims: claims, issuer: _config["Jwt:Issuer"], audience: _config["Jwt:Audience"], expires: DateTime.Now.AddHours(2), signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
