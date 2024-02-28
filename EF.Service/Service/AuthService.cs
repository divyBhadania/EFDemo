using EF.Repository.Interface;
using EF.Repository.Repository;
using EF.Service.DTO;
using EF.Service.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EF.Service.Service
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IConfiguration _jwtConfig;

        public AuthService(IUserRepository userRepository, IUserRoleRepository roleRepository, IConfiguration jwtConfig)
        {
            _userRepository = userRepository;
            _userRoleRepository = roleRepository;
            _jwtConfig = jwtConfig.GetSection("JWTConfig");
        }

        public async Task<ResponseDTO> GetTokenAsync(LoginDTO loginDTO)
        {
            try
            {
                var userId = await _userRepository.CheckUserAuthAsync(loginDTO.Email, loginDTO.Password);
                if (userId != null)
                {
                    return new ResponseDTO()
                    {
                        Status = 200,
                        Data = await generateJWT(userId ?? -1, loginDTO.Email)
                    };
                }
                else
                {
                    return new ResponseDTO()
                    {
                        Status = 404,
                        Message = "Not valid user and password."
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseDTO
                {
                    Status = 400,
                    Message = "Failed",
                    Error = ex.Message
                };
            }
        }

        private async Task<object> generateJWT(int id, string email)
        {
            var now = DateTime.UtcNow;
            var claims = new List<Claim>()
            {
                new Claim("Jti", Guid.NewGuid().ToString()),
                new Claim("Iat", now.ToUniversalTime().ToString(CultureInfo.InvariantCulture), ClaimValueTypes.Integer64),
                new Claim("Id",Convert.ToString(id)),
                new Claim("Emaik",Convert.ToString(email))
            };
            var roles = await _userRoleRepository.GetRolesByIdAsync(id);
            roles.ForEach(x => claims.Add(new Claim("Role", x)));
            var symmetricKeyAsBase64 = _jwtConfig["SecretKey"];
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);
            var jwt = new JwtSecurityToken(
                issuer: _jwtConfig["Issuer"],
                audience: _jwtConfig["Audience"],
                claims: claims,
                notBefore: now,
                expires: now.Add(TimeSpan.FromHours(24)),
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return new
            {
                access_token = "Bearer " + encodedJwt,
                expires_at = now.Add(TimeSpan.FromHours(24))
            };
        }

    }
}
