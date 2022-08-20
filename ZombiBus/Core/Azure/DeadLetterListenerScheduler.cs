using Hangfire;

namespace ZombiBus.Core.Azure;

public class DeadLetterListenerScheduler : IDeadLetterListenerScheduler
{
    public Task Start(DeadLetterConnection connection)
    {
        RecurringJob.AddOrUpdate<QueueDeadLetterJob>(connection.Id.ToString(), j => j.Run(), Cron.Minutely());
        return Task.CompletedTask;
    }

    public Task Stop(DeadLetterConnection deadLetterConnection)
    {
        RecurringJob.RemoveIfExists(deadLetterConnection.Id.ToString());
        return Task.CompletedTask;
    }
}