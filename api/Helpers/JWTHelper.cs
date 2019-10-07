using System;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

using Newtonsoft.Json;

namespace Sample.Helpers
{
    public class JWTHelper
    {
        public static string GenerateToken(string key, int expirationMonth = 1, dynamic extraPayload = null)
        {
            var expirationTime = DateTime.Now.ToUniversalTime().AddMonths(expirationMonth);
            Int32 unixTimestamp = (Int32)(expirationTime.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

            var securityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(key));
            var signingCredentials = new SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha256Signature);

            var header = new JwtHeader(signingCredentials);

            var payload = new JwtPayload
           {
               {"extra", extraPayload},
               {"exp", unixTimestamp},
           };

            var secretToken = new JwtSecurityToken(header, payload);
            var handler = new JwtSecurityTokenHandler();
            var tokenString = handler.WriteToken(secretToken);
            return tokenString;
        }

        public static dynamic DecodeToken(string token)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            return handler.ReadJwtToken(token).Payload;
        }

        public static dynamic GetExtraPayload(string key, string token)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
                byte[] byteArrayOfKey = Encoding.Default.GetBytes(key);
                TokenValidationParameters parameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(byteArrayOfKey),
                    ValidateAudience = false,
                    ValidateIssuer = false,
                };
                SecurityToken securityToken;
                ClaimsPrincipal payload = tokenHandler.ValidateToken(token,
                      parameters, out securityToken);
                return JsonConvert.DeserializeObject(payload.FindFirstValue("extra"));
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}