using Azure.Messaging.ServiceBus;

namespace ZombiBus.Core.Azure;

public class DestroyableDeadLetter : DeadLetter
{
    private readonly ServiceBusReceiver _receiver;
    public DeadLetter DeadLetter { get; }
    private readonly ServiceBusReceivedMessage _message;

    public DestroyableDeadLetter(
        int connectionId,
        DateTime runTime,
        ServiceBusReceiver receiver,
        ServiceBusReceivedMessage message)
    {
        _message = message;
        _receiver = receiver;

        DeadLetter = new DeadLetter
        {
            Content = message.Body.ToString(),
            CollectedAt = runTime,
            Metadata = System.Text.Json.JsonSerializer.Serialize(message),
            ConnectionId = connectionId,
        };
    }

    public async Task Destroy()
    {
        await _receiver.CompleteMessageAsync(_message);
    }
}