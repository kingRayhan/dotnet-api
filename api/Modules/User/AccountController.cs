using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using api.Common;
using api.Common.Services;
using api.Data;
using api.Entities;
using api.Modules.User.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace api.Modules.User;

public class AccountController: ApiBaseController
{
    private readonly DatabaseContext _dbContext;
    private readonly IConfiguration _config;
    private readonly TokenService _tokenService;

    public AccountController(DatabaseContext dbContext, TokenService tokenService)
    {
        _dbContext = dbContext;
        _tokenService = tokenService;
    }
    
    
    [HttpPost]
    public ActionResult<string> Register([FromBody] RegisterUserDto dto)
    {
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        var user = _dbContext.Users.FirstOrDefault(u => u.UserName == dto.UserName);

        if (user != null) return BadRequest("Username already exists");
        
        user = new AppUser
        {
            UserName = dto.UserName,
            PasswordHash = passwordHash
        };
        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();
        
        return Ok(user);
    }
    
    [HttpPost]
    public ActionResult<string> Login([FromBody] LoginUserDto dto)
    {
        var user = _dbContext.Users.SingleOrDefault(u => u.UserName == dto.UserName);
        if (user == null) return Unauthorized();
        
        if(!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash)) return Unauthorized();
        
        
        return Ok(new
        {
            token = _tokenService.GenerateToken(new GenerateTokenPayload()
            {
                UserId = user.Id.ToString(),
                UserName = user.UserName
            }),
            issuer = _config["Jwt:Issuer"],
            issuedAt = DateTime.Now,
            user
        });
    }
}