using Microsoft.EntityFrameworkCore;
using ZombiBus.Core;

namespace ZombiBus.Persistance;

public class ZombiDbContext : DbContext
{
    public ZombiDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<DeadLetterConnection> DeadLetterConnections { get; set; }
    public DbSet<DeadLetter> DeadLetters { get; set; }
}