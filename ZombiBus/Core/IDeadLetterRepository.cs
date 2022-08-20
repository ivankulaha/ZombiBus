namespace ZombiBus.Core;

public interface IDeadLetterRepository
{
    public void Add(DeadLetter deadLetter);
    public Task SaveChanges();
}