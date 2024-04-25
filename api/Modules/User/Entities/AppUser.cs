using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Modules.User.Entities;

[Table("users")]
public class AppUser 
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("userName")]
    public string? UserName { get; set; }
    
    [Column("email")]
    public string? Email { get; set; }
    
    [Column("passwordHash")]
    public string? PasswordHash { get; set; }
}