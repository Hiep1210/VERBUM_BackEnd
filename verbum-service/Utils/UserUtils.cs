using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace verbum_service.Utils
{
    public class UserUtils
    {
        //public static string GenerateJSONWebToken(User userInfo)
        //{
        //    var _config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        //    var claims = new[] {
        //        new Claim("username", userInfo.Username),
        //        new Claim("pass", userInfo.Password),
        //        //new Claim(ClaimTypes.Role, userInfo.Role.ToString()),
        //        //new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        //    };

        //    var token = new JwtSecurityToken(
        //      claims: claims,
        //      expires: DateTime.Now.AddHours(3),
        //      audience: _config["Jwt:Audience"],
        //      issuer: _config["Jwt:Issuer"],
        //      signingCredentials: credentials);

        //    //HttpContext.Session.SetString("token", token.ToString());

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}
    }
}
