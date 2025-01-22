using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Identity.API.Helpers;

public static class TokenHelpers
{
    public static string GenerateToken(IConfiguration conf)
    {
        try
        {
            var jwtConfiguration = conf.GetSection("JwtConfiguration");
            
            ArgumentNullException.ThrowIfNull(jwtConfiguration);

            var issuer = jwtConfiguration.GetSection("Issuer").Value ?? string.Empty;
            var audience = jwtConfiguration.GetSection("Audience").Value ?? string.Empty;

            DateTime expires = DateTime.Now.AddMinutes(
                int.Parse(
                    jwtConfiguration.GetSection("ExpirationTimeInMinutes").Value ?? string.Empty
                )
            );

            var securityKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        jwtConfiguration.GetSection("SecurityKey").Value ?? string.Empty
                    )
                );

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                expires: expires,
                signingCredentials: credentials
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }
        catch (Exception e)
        {
            throw;
        }
    }
    
}