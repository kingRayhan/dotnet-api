using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace IdentityUtility.Token;

public class TokenService(TokenServiceConfig _config) : ITokenService
{
    public string GenerateToken(GenerateTokenPayload payload)
    {

        Console.WriteLine("------------ GenerateToken -----------");
        Console.WriteLine($"UserId: {payload.UserId}");
        Console.WriteLine($"UserName: {payload.DisplayName}");
        Console.WriteLine(" ... ");
        Console.WriteLine($"Secret: {_config.Secret}");
        Console.WriteLine($"Audience: {_config.Audience}");
        Console.WriteLine($"Issuer: {_config.Issuer}");
        Console.WriteLine("--------------------------------------");
        
        
        
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Secret));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        
        var Sectoken = new JwtSecurityToken(_config.Issuer, _config.Audience,
            [
                new Claim("sub", payload.UserId), 
                new Claim("displayName", payload.DisplayName ?? ""),
                new Claim("email", payload.Email ?? "")
            ],
            expires: DateTime.Now.AddMinutes(120), signingCredentials: credentials);
        
        var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);
        return token;
    }
}

public class GenerateTokenPayload
{
    public string? UserId { get; set; }
    public string? DisplayName { get; set; }
    public string? Email { get; set; }
}

public class TokenServiceConfig
{
    public string? Issuer { get; set; }
    public string? Audience { get; set; }
    public string? Secret { get; set; }
}