using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTProject
{
    public class JWTAuthenticationManager : IJWTAuthenticationManager
    {



        public string Authenticate(string username, string password)
        {
            if (!UserManager.Users.Any(x => x.Key == username && x.Value.Password == password))
            {
                return null;
            }

            User user = UserManager.Users.FirstOrDefault(x=> x.Key == username).Value;

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("bu benim key değerim.");
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Role,"person"));
            claims.Add(new Claim(ClaimTypes.Role, user.GroupName));
            var appIdentity = new ClaimsIdentity(claims);
            var tokenDescripter = new SecurityTokenDescriptor
            {
                Subject =appIdentity,
                Expires = DateTime.UtcNow.AddDays(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescripter);

            return tokenHandler.WriteToken(token);
        }
    }

}
