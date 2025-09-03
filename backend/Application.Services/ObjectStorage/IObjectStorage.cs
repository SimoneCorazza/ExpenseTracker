namespace ExpenseTracker.Application.Services.ObjectStorage;

public interface IObjectStorage
{
    Task Upload(byte[] data, Guid? objectId);

    Task<byte[]?> Download(Guid id);

    Task Delete(Guid id);
}
