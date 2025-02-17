using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Identity.API.Helpers;

public static class TokenHelpers
{
    public static string GenerateToken(IConfiguration conf , IEnumerable<string> roles, Guid userId )
    {
        try
        {
            var jwtConfiguration = conf.GetSection("JwtConfiguration");
            
            ArgumentNullException.ThrowIfNull(jwtConfiguration);

            var issuer = jwtConfiguration.GetSection("Issuer").Value ?? string.Empty;
            var audience = jwtConfiguration.GetSection("Audience").Value ?? string.Empty;
            var audiencesAsClaimList = audience.Split(';').Select(s => new Claim("aud", s));
            var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r));  
            var claims = audiencesAsClaimList.Concat(roleClaims);
            claims = claims.Append(new Claim("sub", userId.ToString()));
            
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
                audience: null,//multiples audiences must be passed as claim list
                expires: expires,
                signingCredentials: credentials,
                claims: claims
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