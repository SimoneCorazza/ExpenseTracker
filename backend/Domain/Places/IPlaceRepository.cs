namespace ExpenseTracker.Domain.Places;

public interface IPlaceRepository : IRepository<Place>
{
    /// <summary>
    ///     Get all places for the given user
    /// </summary>
    /// <param name="userId">User id</param>
    /// <returns>List of places</returns>
    Task<ICollection<Place>> GetFor(Guid userId);

    /// <summary>
    ///     Get a place by its ID
    /// </summary>
    /// <param name="id">Place id</param>
    /// <returns>Place or null if not found</returns>
    Task<Place?> GetById(Guid id);
}
