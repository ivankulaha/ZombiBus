namespace ZombiBus.Core;

public class DeadLetterConnection
{
    public int Id { get; set; }
    public string QueueName { get; set; }
    public string SubscriptionName { get; set; }
    public string OriginQueueName { get; set; }
    public string OriginSubscriptionName { get; set; }
    public string ConnectionString { get; set; }
}