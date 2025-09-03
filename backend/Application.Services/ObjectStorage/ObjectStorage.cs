using Minio;
using Minio.DataModel.Args;

namespace ExpenseTracker.Application.Services.ObjectStorage;

public class ObjectStorage : IObjectStorage
{
    private readonly IMinioClient minioClient;
    private readonly string bucketName;

    public ObjectStorage(IMinioClient minioClient, string bucketName)
    {
        this.minioClient = minioClient;
        this.bucketName = bucketName;

        // Create the bucket if not exists
        if (!minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(bucketName)).Result)
        {
            minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(bucketName)).Wait();
        }
    }

    public async Task Delete(Guid id)
    {
        await minioClient.RemoveObjectAsync(new RemoveObjectArgs()
            .WithBucket(bucketName)
            .WithObject(id.ToString()));
    }

    public async Task<byte[]?> Download(Guid id)
    {
        byte[]? data = null;
        var obj = new GetObjectArgs()
            .WithBucket(bucketName)
            .WithObject(id.ToString())
            .WithCallbackStream(stream =>
            {
                using var ms = new MemoryStream();
                stream.CopyTo(ms);
                ms.Position = 0; // Reset stream position to enable re-read
                data = ms.ToArray();
            });

        var response = await minioClient.GetObjectAsync(obj);

        return data;
    }

    public async Task Upload(byte[] data, Guid? objectId)
    {
        var par = new PutObjectArgs()
            .WithContentType("application/pdf") // TODO: remove?
            .WithBucket(bucketName)
            .WithObject(objectId.ToString())
            .WithStreamData(new MemoryStream(data))
            .WithObjectSize(data.Length);

        await minioClient.PutObjectAsync(par);
    }
}
