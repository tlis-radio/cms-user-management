using EntityFramework.Exceptions.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using Tlis.Cms.UserManagement.Domain.Entities;
using Tlis.Cms.UserManagement.Infrastructure.Persistence.Interfaces;

namespace Tlis.Cms.UserManagement.Infrastructure.Persistence;

public class UserManagementDbContext(DbContextOptions options)
    : DbContext(options), IUserManagementDbContext
{
    public DbSet<Role> Roles { get; set; }

    public readonly static string SCHEMA = "cms_user_management";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseExceptionProcessor();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserManagementDbContext).Assembly);
        modelBuilder.HasDefaultSchema(SCHEMA);
        base.OnModelCreating(modelBuilder);
    }
}