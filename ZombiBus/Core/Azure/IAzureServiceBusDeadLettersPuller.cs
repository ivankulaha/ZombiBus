namespace ZombiBus.Core.Azure;

public interface IAzureServiceBusDeadLettersPuller : IAsyncDisposable
{
    Task<List<DestroyableDeadLetter>> Pull(DeadLetterConnection connection);
}