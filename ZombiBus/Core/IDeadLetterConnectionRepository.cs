namespace ZombiBus.Core;

public interface IDeadLetterConnectionRepository
{
    void Add(DeadLetterConnection entity);
    void Remove(DeadLetterConnection entity);
    Task SaveChanges();
    Task<IEnumerable<DeadLetterConnection>> GetAll();
    Task<DeadLetterConnection?> Find(int id);
    void Update(DeadLetterConnection connection);
}