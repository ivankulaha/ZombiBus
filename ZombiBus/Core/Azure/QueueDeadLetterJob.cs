namespace ZombiBus.Core.Azure;

public class QueueDeadLetterJob
{
    private readonly IDeadLetterRepository _deadLetterRepository;
    private readonly IAzureServiceBusDeadLettersPuller _puller;
    private readonly IDeadLetterConnectionRepository _repository;

    public QueueDeadLetterJob(
        IDeadLetterRepository deadLetterRepository,
        IAzureServiceBusDeadLettersPuller puller,
        IDeadLetterConnectionRepository repository)
    {
        _deadLetterRepository = deadLetterRepository;
        _puller = puller;
        _repository = repository;
    }

    public async Task Run()
    {
        var connections = await _repository.GetAll();
        foreach (var connection in connections)
        {
            var deadLetters = await _puller.Pull(connection);
            foreach (var deadLetter in deadLetters)
            {
                _deadLetterRepository.Add(deadLetter);
            }
        }

        await _repository.SaveChanges();
    }
}