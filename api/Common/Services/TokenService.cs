using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace api.Common.Services;

public class TokenService
{
    private readonly IConfiguration _config;

    public TokenService(IConfiguration config)
    {
        _config = config;
    }

    public string GenerateToken(GenerateTokenPayload payload)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Secret"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var Sectoken = new JwtSecurityToken(_config["Jwt:Issuer"],
            _config["Jwt:Issuer"],
            [
                new Claim(ClaimTypes.Sid, payload.UserId),
                new Claim(ClaimTypes.Name, payload.UserId ?? ""),
                new Claim(ClaimTypes.Role, "app-user")
            ],
            expires: DateTime.Now.AddMinutes(120),
            signingCredentials: credentials);
        
        
        var token =  new JwtSecurityTokenHandler().WriteToken(Sectoken);
        return token;
    }
}

public class GenerateTokenPayload
{
    public string? UserId { get; set; }
    public string? UserName { get; set; }
}