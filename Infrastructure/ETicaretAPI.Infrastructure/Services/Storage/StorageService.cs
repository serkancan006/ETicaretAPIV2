using ETicaretAPI.Application.Abstractions.Storage;
using Microsoft.AspNetCore.Http;

namespace ETicaretAPI.Infrastructure.Services.Storage
{
    public class StorageService(IStorage storage) : IStorageService
    {
        public string StorageName { get => storage.GetType().Name; }

        public async Task DeleteAsync(string pathOrContainerName, string fileName)
            => await storage.DeleteAsync(pathOrContainerName, fileName);

        public List<string> GetFiles(string pathOrContainerName)
            => storage.GetFiles(pathOrContainerName);

        public bool HasFile(string pathOrContainerName, string fileName)
            => storage.HasFile(pathOrContainerName, fileName);

        public Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainerName, IFormFileCollection files)
            => storage.UploadAsync(pathOrContainerName, files);
    }
}
