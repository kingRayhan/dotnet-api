using api.Common;
using api.Common.Dtos;
using api.Common.ReferenceModel;
using api.Data;
using api.Modules.User.Dtos;
using api.Modules.User.Entities;
using Faker;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Modules.User;

public class UsersController(DatabaseContext dbContext) : ApiBaseController
{
    [HttpGet]
    // public PaginatedResponse<AppUser> GetAllUsers([FromQuery] CommonPaginationQueryDto queryDto)
    public async Task<ActionResult<PaginatedResponse<AppUser>>> GetAllUsers([FromQuery] CommonPaginationQueryDto queryDto)
    {
        var pageSize = queryDto.Limit ?? 10;
        var pageNumber = queryDto.Page ?? 1;
        var totalCount = dbContext.Users.Count();
        var users = await dbContext.Users.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        
        return Ok(new PaginatedResponse<AppUser>
        {
            Metadata = new PaginationMetadata
            {
                Total = totalCount,
                Limit = pageSize,
                Page = pageNumber
            },
            Items = users
        });
    }
    
    [HttpGet("{id}")]
    public ActionResult<AppUser> GetUserById(int id)
    {
        var user = dbContext.Users.FirstOrDefault(u => u.Id == id);
        if (user == null) return NotFound("User not found"); // TODO: Add custom error message
        
        return Ok(user);
    }
    
    [HttpPost]
    public ActionResult CreateUser([FromBody] CreateUserDto dto)
    {
        var user = new AppUser
        {
            UserName = dto.UserName,
            PasswordHash = dto.Password
        };
        dbContext.Users.Add(user);
        dbContext.SaveChanges();
        return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
    }
    
    
    [HttpPut("{id}")]
    public ActionResult<AppUser> UpdateUser(int id, [FromBody] UpdateUserDto dto)
    {
        var user = dbContext.Users.FirstOrDefault(u => u.Id == id);
        if (user == null) return NotFound("User not found"); // TODO: Add custom error message
        
        user.UserName = dto.UserName;
        user.PasswordHash = dto.Password;
        dbContext.SaveChanges();
        return Ok(user);
    }
    

    [HttpGet]
    public void seed()
    {
        Enumerable.Range(1, 5000).ToList().ForEach(i =>
        {
            var user = new AppUser
            {
                UserName = Name.FullName(),
                PasswordHash = Internet.SecureUrl(),
            };
            dbContext.Users.Add(user);
        });
        dbContext.SaveChanges();
    }
}