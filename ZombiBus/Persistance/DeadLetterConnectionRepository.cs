using Microsoft.EntityFrameworkCore;
using ZombiBus.Core;

namespace ZombiBus.Persistance;

public class DeadLetterConnectionRepository : IDeadLetterConnectionRepository
{
    private readonly ZombiDbContext _dbContext;

    public DeadLetterConnectionRepository(ZombiDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(DeadLetterConnection entity)
    {
        _dbContext.Add(entity);
    }

    public Task SaveChanges()
    {
        return _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<DeadLetterConnection>> GetAll()
    {
        return await _dbContext.DeadLetterConnections.ToListAsync();
    }
}