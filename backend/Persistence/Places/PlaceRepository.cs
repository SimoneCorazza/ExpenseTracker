using ExpenseTracker.Domain.Places;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Persistence.Places;

internal class PlaceRepository : BaseRepository<Place>, IPlaceRepository
{
    public PlaceRepository(DbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<Place?> GetById(Guid id)
        => await DbContext.Places.SingleOrDefaultAsync(p => p.Id == id);

    public async Task<ICollection<Place>> GetFor(Guid userId)
        => await DbContext.Places
            .Where(p => p.UserId == userId)
            .ToListAsync();
}
