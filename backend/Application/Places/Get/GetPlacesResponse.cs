namespace ExpenseTracker.Application.Places.Get;

public class GetPlacesResponse
{
    public ICollection<Place> Places { get; set; } = [];
}