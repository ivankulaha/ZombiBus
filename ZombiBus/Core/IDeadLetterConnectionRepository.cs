namespace ZombiBus.Core;

public interface IDeadLetterConnectionRepository
{
    void Add(DeadLetterConnection entity);
    Task SaveChanges();
    Task<IEnumerable<DeadLetterConnection>> GetAll();
}