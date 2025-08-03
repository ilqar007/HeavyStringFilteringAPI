using HeavyStringFiltering.Application.Services;
using System.Collections.Concurrent;

namespace HeavyStringFiltering.Infrastructure.Services
{
    public class BackgroundQueueService : IBackgroundQueueService
    {
        private readonly ConcurrentQueue<Func<CancellationToken, ValueTask<string>>> _queue = new ConcurrentQueue<Func<CancellationToken, ValueTask<string>>>();

        public BackgroundQueueService()
        {
        }

        public async ValueTask EnqueueAsync(
            Func<CancellationToken, ValueTask<string>> workItem)
        {
            await Task.Run(() =>
             {
                 if (workItem == null)
                 {
                     throw new ArgumentNullException(nameof(workItem));
                 }

                 _queue.Enqueue(workItem);
             });
        }

        public async ValueTask<Func<CancellationToken, ValueTask<string>>> DequeueAsync(
            CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
              {
                  _queue.TryDequeue(out var workItem);
                  return workItem;
              });
        }
    }
}