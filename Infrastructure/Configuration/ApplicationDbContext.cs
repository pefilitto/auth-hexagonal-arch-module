using auth_hexagonal_arch_module.Application.Entities;
using Microsoft.EntityFrameworkCore;

namespace auth_hexagonal_arch_module.Infrastructure.Configuration;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; }
}