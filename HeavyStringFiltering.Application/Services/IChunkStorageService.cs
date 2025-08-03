using HeavyStringFiltering.Application.Dtos;

namespace HeavyStringFiltering.Application.Services
{
    public interface IChunkStorageService
    {
        Task ClearChunksAsync(string uploadId);

        Task<string> CombineAllChunksAsync(string uploadId);

        Task StoreChunkAsync(TextChunkDto chunkDto);
    }
}