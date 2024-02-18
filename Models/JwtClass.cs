using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTauthentication_practice.Models
{
    public class JwtClass
    {
        public string Key { get; set; }
        public string Duration { get; set; }

        public JwtClass(string key, string duration)
        {
            this.Key = key;
            this.Duration = duration;
        }


        public string generateToken(UserClass user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.Key));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim( "UserId", user.UserId.ToString()),
                new Claim("Username", user.UserName),
                new Claim("Password", user.Password)
            };
            var jwtToken = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddMinutes(Int32.Parse(this.Duration)),
                signingCredentials: credential);   
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}
