namespace ExpenseTracker.Application.Places.Get;

public class GetPlacesResponse
{
    public ICollection<PlaceDto> Places { get; set; } = [];
}