using ETicaretAPI.Application.Abstractions.Storage.Minio;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;
using Minio.ApiEndpoints;


namespace ETicaretAPI.Infrastructure.Services.Storage.Minio
{
    public class MinioStorage : Storage, IMinioStorage
    {
        private readonly IMinioClient _minioClient;

        public MinioStorage(IConfiguration configuration)
        {
            _minioClient = new MinioClient()
                .WithEndpoint(configuration["Storage:Minio:Endpoint"],
                              int.Parse(configuration["Storage:Minio:Port"]))
                .WithCredentials(configuration["Storage:Minio:AccessKey"],
                                 configuration["Storage:Minio:SecretKey"])
                .WithSSL(bool.Parse(configuration["Storage:Minio:UseSSL"]))
                .Build();
        }

        public async Task DeleteAsync(string bulletName, string fileName)
        {
            var args = new RemoveObjectArgs()
                .WithBucket(bulletName)
                .WithObject(fileName);
            await _minioClient.RemoveObjectAsync(args);
        }

        public List<string> GetFiles(string bulletName)
        {
            var files = new List<string>();

            var args = new ListObjectsArgs()
                .WithBucket(bulletName)
                .WithRecursive(true);

            var observable = _minioClient.ListObjectsAsync(args);
            var resetEvent = new ManualResetEventSlim(false);

            observable.Subscribe(item =>
            {
                if (item?.Key != null)
                {
                    files.Add(item.Key);
                }
            },
            ex => throw ex,
            () => resetEvent.Set());

            resetEvent.Wait();

            return files;
        }

        public bool HasFile(string bulletName, string fileName)
        {
            try
            {
                var args = new StatObjectArgs()
                    .WithBucket(bulletName)
                    .WithObject(fileName);
                _minioClient.StatObjectAsync(args).GetAwaiter().GetResult();
                return true;
            }
            catch (MinioException)
            {
                return false;
            }
        }

        public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string bulletName, IFormFileCollection files)
        {
            var datas = new List<(string fileName, string pathOrContainerName)>();

            // Bucket kontrolü ve oluşturulması
            var bucketExistsArgs = new BucketExistsArgs().WithBucket(bulletName);
            bool bucketExists = await _minioClient.BucketExistsAsync(bucketExistsArgs);
            if (!bucketExists)
            {
                var makeBucketArgs = new MakeBucketArgs().WithBucket(bulletName);
                await _minioClient.MakeBucketAsync(makeBucketArgs);
            }

            // Dosya yükleme
            foreach (IFormFile file in files)
            {
                // `FileRenameAsync`'i burada kullanıyoruz
                string fileNewName = await FileRenameAsync(bulletName, file.FileName, HasFile);

                using var stream = file.OpenReadStream();
                var putObjectArgs = new PutObjectArgs()
                    .WithBucket(bulletName)
                    .WithObject(fileNewName)
                    .WithStreamData(stream)
                    .WithObjectSize(stream.Length)
                    .WithContentType(file.ContentType);

                await _minioClient.PutObjectAsync(putObjectArgs);

                datas.Add((fileNewName, $"{bulletName}/{fileNewName}"));
            }

            return datas;
        }

    }
}
