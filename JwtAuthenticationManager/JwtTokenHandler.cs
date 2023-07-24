using JwtAuthenticationManager.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtAuthenticationManager
{
    public class JwtTokenHandler
    {
        public const string JWT_SECURITY_KEY = "qwert73ioplkjhgfd8SDFGkjGUnlihDh0";
        private const int JWT_TOKEN_VALIDITY_MINS = 20;
        private readonly List<User> _userList;
        public JwtTokenHandler()
        {
            _userList = new List<User>
            {
                new User{Name="admin", Password="admin123", Role="Administrator"},
                new User{Name="userrr", Password="user123", Role="User" }
            };
        }

        public AuthenticationResponse? GenerateJwtToken(AuthenticationRequest authenticationRequest)
        {
            if (string.IsNullOrWhiteSpace(authenticationRequest.Name) || string.IsNullOrWhiteSpace(authenticationRequest.Password))
                return null;

            var user = _userList.Where(x => x.Name == authenticationRequest.Name && x.Password == authenticationRequest.Password).FirstOrDefault();
            if (user == null) return null;

            var rokenExpirtyTimeStamp = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MINS);
            var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);
            var claimsIdentity = new ClaimsIdentity(new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Name, authenticationRequest.Name),
                new Claim(ClaimTypes.Role, user.Role)
            });

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature);

            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = rokenExpirtyTimeStamp,
                SigningCredentials = signingCredentials
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);

            return new AuthenticationResponse
            {
                Name = user.Name,
                ExpiresIn = (int)rokenExpirtyTimeStamp.Subtract(DateTime.Now).TotalSeconds,
                JwtToken = token
            };
        }
    }
}
