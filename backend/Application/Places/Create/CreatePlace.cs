using MediatR;

namespace ExpenseTracker.Application.Places.Create;

public class CreatePlace : IRequest<CreatePlaceResponse>
{
    public string Name { get; set; }

    public string? Description { get; set; }
}