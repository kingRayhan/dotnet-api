using api.Modules.User.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Data;

public class DatabaseContext: DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }
    
    public DbSet<AppUser> Users { get; set; }
}