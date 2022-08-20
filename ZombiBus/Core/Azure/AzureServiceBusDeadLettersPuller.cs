using Azure.Messaging.ServiceBus;

namespace ZombiBus.Core.Azure;

public class AzureServiceBusDeadLettersPuller : IAzureServiceBusDeadLettersPuller
{
    public async Task<List<DeadLetter>> Pull(DeadLetterConnection connection)
    {
        var runTime = DateTime.UtcNow;
        await using var client = new ServiceBusClient(connection.ConnectionString);
        await using var receiver = client.CreateReceiver(connection.QueueName + "/$deadletterqueue");

        IReadOnlyList<ServiceBusReceivedMessage> fetched;
        var deadLetters = new List<DeadLetter>();
        do
        {
            fetched = await receiver.ReceiveMessagesAsync(maxMessages: 50);
            foreach (var message in fetched)
            {
                deadLetters.Add(new DeadLetter
                {
                    Content = message.Body.ToString(),
                    CollectedAt = runTime,
                    Metadata = System.Text.Json.JsonSerializer.Serialize(message),
                    ConnectionId = connection.Id,
                });
            }
        }
        while (fetched.Any());

        return deadLetters;
    }
}