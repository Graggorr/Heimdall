using Heimdall.Modules.Users.Domain;
using Microsoft.EntityFrameworkCore;

namespace Heimdall.Modules.Users.Data;

internal sealed class UsersDbContext(DbContextOptions<UsersDbContext> options) : DbContext(options)
{
    public const string Schema = "users";

    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema);
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
