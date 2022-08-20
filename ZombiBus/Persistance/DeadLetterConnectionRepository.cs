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

    public void Remove(DeadLetterConnection entity)
    {
        _dbContext.Remove(entity);
    }

    public Task SaveChanges()
    {
        return _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<DeadLetterConnection>> GetAll()
    {
        return await _dbContext.DeadLetterConnections.ToListAsync();
    }

    public async Task<DeadLetterConnection?> Find(int id)
    {
        return await _dbContext.DeadLetterConnections.FindAsync(id);
    }

    public void Update(DeadLetterConnection connection)
    {
        _dbContext.Attach(connection);
    }
}