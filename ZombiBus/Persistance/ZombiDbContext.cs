using Microsoft.EntityFrameworkCore;
using ZombiBus.Core;

namespace ZombiBus.Persistance;

public class ZombiDbContext : DbContext
{
    public DbSet<DeadLetterConnection> Zombies { get; set; }
}