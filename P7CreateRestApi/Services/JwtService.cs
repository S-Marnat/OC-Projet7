using P7CreateRestApi.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using P7CreateRestApi.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace P7CreateRestApi.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;

        public JwtService(IConfiguration configuration, UserManager<User> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<string> GenererTokenAsync(User user)
        {
            // Récupération des rôles de l’utilisateur
            var roles = await _userManager.GetRolesAsync(user);

            // Création des claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("fullname", user.Fullname)
            };

            // Ajout des rôles dans les claims
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Création de la clé de signature
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _configuration["Jwt:Key"]
                    ?? Environment.GetEnvironmentVariable("JWT_SECRET_KEY")
                )
            );

            // Création des informations de signature du token
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Construction du token JWT
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(
                    Convert.ToDouble(_configuration["Jwt:DurationInMinutes"])
                ),
                signingCredentials: creds
            );

            // Conversion du token en string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
