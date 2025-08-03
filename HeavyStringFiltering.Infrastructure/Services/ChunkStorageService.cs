using HeavyStringFiltering.Application.Dtos;
using HeavyStringFiltering.Application.Services;
using System.Collections.Concurrent;
using System.Text;

namespace HeavyStringFiltering.Infrastructure.Services
{
    public class ChunkStorageService : IChunkStorageService
    {
        private static readonly ConcurrentDictionary<string, SortedList<int, string>> chunksDict = new();

        public async Task ClearChunksAsync(string uploadId)
        {
            await Task.Run(() =>
              {
                  chunksDict.TryRemove(uploadId, out _);
              }).ConfigureAwait(false);
        }

        public async Task<string> CombineAllChunksAsync(string uploadId)
        {
            return await Task.Run(() =>
              {
                  if (chunksDict.TryGetValue(uploadId, out SortedList<int, string> chunks))
                  {
                      // Initialize a StringBuilder instance
                      StringBuilder sb = new StringBuilder();
                      // Append strings or other data types
                      foreach (var item in chunks)
                      {
                          sb.Append(item.Value);
                      }
                      // Get the final concatenated string
                      return sb.ToString();
                  }
                  return string.Empty;
              }).ConfigureAwait(false);
        }

        public async Task StoreChunkAsync(TextChunkDto chunkDto)
        {
            await Task.Run(() =>
             {
                 chunksDict.AddOrUpdate(chunkDto.UploadId, (key) => { var sortedList = new SortedList<int, string>(); sortedList.Add(chunkDto.ChunkIndex, chunkDto.Data); return sortedList; }, (key, value) =>
                 {
                     if (!value.ContainsKey(chunkDto.ChunkIndex))
                     {
                         value.Add(chunkDto.ChunkIndex, chunkDto.Data);
                     }
                     return value;
                 });
             }).ConfigureAwait(false);
        }
    }
}