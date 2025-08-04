using HeavyStringFiltering.Application.Dtos;
using HeavyStringFiltering.Application.Services;

namespace HeavyStringFiltering.Infrastructure.Services
{
    public class UploadService : IUploadService
    {
        private readonly IChunkStorageService _chunkStorageService;
        private readonly IBackgroundQueueService _backgroundQueueService;

        public UploadService(IChunkStorageService chunkStorageService, IBackgroundQueueService backgroundQueueService)
        {
            _chunkStorageService = chunkStorageService;
            _backgroundQueueService = backgroundQueueService;
        }

        public async Task UploadChunkAsync(TextChunkDto chunk)
        {
            await _chunkStorageService.StoreChunkAsync(chunk);
            if (chunk.IsLastChunk)
            {
                string fullText = await _chunkStorageService.CombineAllChunksAsync(chunk.UploadId);
                await _backgroundQueueService.EnqueueAsync((ct) => { return new ValueTask<string>(fullText); });
                await _chunkStorageService.ClearChunksAsync(chunk.UploadId); // Clean up temporary storage
            }
        }
    }
}