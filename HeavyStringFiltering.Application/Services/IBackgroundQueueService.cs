namespace HeavyStringFiltering.Application.Services
{
    public interface IBackgroundQueueService
    {
        ValueTask EnqueueAsync(Func<CancellationToken, ValueTask<string>> workItem);

        ValueTask<Func<CancellationToken, ValueTask<string>>> DequeueAsync(CancellationToken cancellationToken);
    }
}