using EFCoreInterceptorExample.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCoreInterceptorExample.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Book> Books { get; set; } = null!;
    public DbSet<Log> Logs { get; set; } = null!;
}
