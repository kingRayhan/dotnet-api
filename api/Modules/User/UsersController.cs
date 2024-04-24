using api.Common;
using api.Common.Dtos;
using api.Common.ReferenceModel;
using api.Data;
using api.Entities;
using api.Modules.User.Dtos;
using Faker;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Modules.User;

public class UsersController: ApiBaseController
{
    private readonly DatabaseContext _dbContext;

    public UsersController(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    [HttpGet]
    // public PaginatedResponse<AppUser> GetAllUsers([FromQuery] CommonPaginationQueryDto queryDto)
    public async Task<ActionResult<PaginatedResponse<AppUser>>> GetAllUsers([FromQuery] CommonPaginationQueryDto queryDto)
    {
        var pageSize = queryDto.Limit ?? 10;
        var pageNumber = queryDto.Page ?? 1;
        var totalCount = _dbContext.Users.Count();
        var users = await _dbContext.Users.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        
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
        var user = _dbContext.Users.FirstOrDefault(u => u.Id == id);
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
        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();
        return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
    }
    
    
    [HttpPut("{id}")]
    public ActionResult<AppUser> UpdateUser(int id, [FromBody] UpdateUserDto dto)
    {
        var user = _dbContext.Users.FirstOrDefault(u => u.Id == id);
        if (user == null) return NotFound("User not found"); // TODO: Add custom error message
        
        user.UserName = dto.UserName;
        user.PasswordHash = dto.Password;
        _dbContext.SaveChanges();
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
            _dbContext.Users.Add(user);
        });
        _dbContext.SaveChanges();
    }
}