using Azure.Messaging.ServiceBus;

namespace ZombiBus.Core.Azure;

public class AzureServiceBusDeadLettersPuller : IAzureServiceBusDeadLettersPuller, IAsyncDisposable
{
    private ServiceBusReceiver? _receiver;
    private ServiceBusClient _client;

    public async Task<List<DestroyableDeadLetter>> Pull(DeadLetterConnection connection)
    {
        var runTime = DateTime.UtcNow;
        _client = new ServiceBusClient(connection.ConnectionString);
        _receiver = _client.CreateReceiver(connection.QueueName + "/$deadletterqueue");

        IReadOnlyList<ServiceBusReceivedMessage> fetched;
        var deadLetters = new List<DestroyableDeadLetter>();
        fetched = await _receiver.ReceiveMessagesAsync(maxMessages: 50, TimeSpan.FromSeconds(30));
        foreach (var message in fetched)
        {
            deadLetters.Add(new DestroyableDeadLetter(connection.Id, runTime, _receiver, message));
        }

        return deadLetters;
    }

    public async ValueTask DisposeAsync()
    {
        await _receiver!.DisposeAsync();
        await _client!.DisposeAsync();
    }
}