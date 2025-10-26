namespace ExpenseTracker.Application.Services.ObjectStorage;

/// <summary>
///     Interface for a generic object storage service.
/// </summary>
public interface IObjectStorage
{
    /// <summary>
    ///     Upload a new object to the storage
    /// </summary>
    /// <param name="data">Data of the storage</param>
    /// <param name="objectId">Id of the object to be created</param>
    /// <param name="contentType">Content type</param>
    /// <param name="tags">Optional tags</param>
    Task Upload(byte[] data, Guid objectId, string contentType, Dictionary<string, string>? tags = null);

    Task<byte[]?> Download(Guid id);

    Task Delete(Guid id);
}
