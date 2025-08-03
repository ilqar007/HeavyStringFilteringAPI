using HeavyStringFiltering.Infrastructure.Services;

namespace HeavyStringFilteringAPI.Tests
{
    public class BackgroundQueueServiceTests
    {
        [Fact]
        public async Task EnqueueAndDequeue_SingleItem_Success()
        {
            // Arrange
            var queue = new BackgroundQueueService();
            var workItem = new Func<CancellationToken, ValueTask<string>>(_ => ValueTask.FromResult(string.Empty));

            // Act
            await queue.EnqueueAsync(workItem);
            var dequeuedItem = await queue.DequeueAsync(CancellationToken.None);
            //Assert
            Assert.NotNull(dequeuedItem);
        }

        [Fact]
        public async Task Dequeue_EmptyQueue_WaitsForEnqueue()
        {
            // Arrange
            var queue = new BackgroundQueueService();
            var workItem = new Func<CancellationToken, ValueTask<string>>(_ => ValueTask.FromResult(string.Empty));

            var dequeueTask = queue.DequeueAsync(CancellationToken.None);

            // Assert that the dequeue task is not completed yet
            Assert.False(dequeueTask.IsCompleted);

            // Act
            await queue.EnqueueAsync(workItem);

            // Assert that the dequeue task is now completed
            await dequeueTask;
            Assert.True(dequeueTask.IsCompletedSuccessfully);
        }
    }
}