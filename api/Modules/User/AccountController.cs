using api.Common;
using api.Data;
using api.Modules.User.Dtos;
using api.Modules.User.Entities;
using IdentityUtility.Token;
using IdentityUtility.Encryption;
using Microsoft.AspNetCore.Mvc;

namespace api.Modules.User;

public class AccountController(DatabaseContext dbContext, ITokenService tokenService) : ApiBaseController
{
    [HttpPost]
    public ActionResult<string> Register([FromBody] RegisterUserDto dto)
    {
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        var user = dbContext.Users.FirstOrDefault(u => u.UserName == dto.UserName || u.Email == dto.Email);

        if (user != null) return BadRequest("Username or Email already exists");
        
        user = new AppUser
        {
            UserName = dto.UserName,
            Email = dto.Email,
            PasswordHash = passwordHash
        };
        dbContext.Users.Add(user);
        dbContext.SaveChanges();
        
        return Ok(user);
    }
    
    [HttpPost]
    public ActionResult<string> Login([FromBody] LoginUserDto dto)
    {
        var user = dbContext.Users.SingleOrDefault(u => u.UserName == dto.UserNameOrEmail || u.Email == dto.UserNameOrEmail);
        
        if (user == null) return Unauthorized();
        if(!EncryptionBcrypt.VerifyPassword(dto.Password, user.PasswordHash)) return Unauthorized();

        return Ok(new
        {
            token = tokenService.GenerateToken(new GenerateTokenPayload
            {
                UserId = user.UserName,
                DisplayName = user.UserName,
            }),
            issuedAt = DateTime.Now,
            user
        });
    }
}