namespace ZombiBus.Core.Azure;

public class QueueDeadLetterJob
{
    private readonly IServiceProvider _serviceProvider;

    public QueueDeadLetterJob(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task Run()
    {
        await using var scope = _serviceProvider.CreateAsyncScope();
        var connectionRepository = _serviceProvider.GetRequiredService<IDeadLetterConnectionRepository>();
        var puller = _serviceProvider.GetRequiredService<IAzureServiceBusDeadLettersPuller>();
        var deadLetterRepository = _serviceProvider.GetRequiredService<IDeadLetterRepository>();
        
        var connections = await connectionRepository.GetAll();
        foreach (var connection in connections)
        {
            
            List<DestroyableDeadLetter> deadLetters;
            do
            {
                deadLetters = await puller.Pull(connection);
                foreach (var destroyableDeadLetter in deadLetters)
                {
                    deadLetterRepository.Add(destroyableDeadLetter.DeadLetter);
                }

                await connectionRepository.SaveChanges();

                foreach (var destroyableDeadLetter in deadLetters)
                {
                    await destroyableDeadLetter.Destroy();
                }
            } while (deadLetters.Any());

            await puller.DisposeAsync();
        }
    }
}