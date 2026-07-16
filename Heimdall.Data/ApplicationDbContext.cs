using Heimdall.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Heimdall.Data;

internal sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    
    public DbSet<Wallet> Wallets { get; set; }
    
    public DbSet<Transaction> Transactions { get; set; }
}
