using ExpenseTracker.Domain.Places;

namespace ExpenseTracker.Persistence.Places
{
    internal class PlaceRepository : BaseRepository<Place>, IPlaceRepository
    {
        public PlaceRepository(DbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
