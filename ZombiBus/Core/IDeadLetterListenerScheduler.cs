namespace ZombiBus.Core;

public interface IDeadLetterListenerScheduler
{
    Task Start(DeadLetterConnection connection);
    Task Stop(DeadLetterConnection deadLetterConnection);
}