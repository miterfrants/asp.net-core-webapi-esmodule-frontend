using System;
using System.Security.Claims;
using System.Text;

namespace Sample.Helpers
{
    public class JWTHelper
    {
        //private static string Secret = System.Configuration.ConfigurationManager.AppSettings.Get("JWT_SECRET");
        //public static string GenerateToken(string username, int userId, string role, int expireMinutes = 20)
        //{
        //    var expirationDate = DateTime.Now.AddMonths(1);
        //    var securityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(Secret));
        //    var signingCredentials = new SigningCredentials(
        //        securityKey,
        //        SecurityAlgorithms.HmacSha256Signature);

        //    var header = new JwtHeader(signingCredentials);

        //    var payload = new JwtPayload
        //    {
        //        {"role", role},
        //        {"id", userId},
        //        {"username", username},
        //        {"exp", expirationDate.ToUniversalTime()},
        //    };

        //    var secretToken = new JwtSecurityToken(header, payload);
        //    var handler = new JwtSecurityTokenHandler();
        //    var tokenString = handler.WriteToken(secretToken);
        //    return tokenString;
        //}

        //public static object DecodeToken(string token)
        //{
        //    JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        //    return handler.ReadJwtToken(token).Payload;
        //}
        //public static string RefreshToken(string token)
        //{

        //    return "";
        //}
    }
}