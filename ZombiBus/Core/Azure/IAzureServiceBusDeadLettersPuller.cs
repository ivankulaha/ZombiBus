namespace ZombiBus.Core.Azure;

public interface IAzureServiceBusDeadLettersPuller
{
    Task<List<DeadLetter>> Pull(DeadLetterConnection connection);
}