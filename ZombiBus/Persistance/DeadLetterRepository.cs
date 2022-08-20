using ZombiBus.Core;

namespace ZombiBus.Persistance;

public class DeadLetterRepository : IDeadLetterRepository
{
    private readonly ZombiDbContext _dbContext;

    public DeadLetterRepository(ZombiDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(DeadLetter deadLetter)
    {
        _dbContext.Add(deadLetter);
    }

    public async Task SaveChanges()
    {
        await _dbContext.SaveChangesAsync();
    }
}