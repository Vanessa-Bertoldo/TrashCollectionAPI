using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TrashCollectionAPI.Data.Contexts;
using TrashCollectionAPI.Data.Repository;
using TrashCollectionAPI.Models;

namespace TrashCollectionAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _repository;
        public AuthService(IUserRepository repository)
        {
            _repository = repository;
        }
        public void RegisterUser(UserModel user)
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            _repository.RegisterUser(user);
        }

        public TokenUserModel VerificaLogin(AuthModel auth)
        {
            var user = _repository.Autenticacao(auth);
            return new TokenUserModel
            {
                UserId = user.UserId,
                Username = user.Username,
                Token = GenerateJwtToken(user)
            };
        }

        private string GenerateJwtToken(UserModel user)
        {
            byte[] secret = Encoding.ASCII.GetBytes("f+ujXAKHk00L5jlMXo2XhAWawsOoihNP1OiAM25lLSO57+X7uBMQgwPju6yzyePi");
            var securityKey = new SymmetricSecurityKey(secret);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim(ClaimTypes.Hash, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                Issuer = "fiap",
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(secret),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}

