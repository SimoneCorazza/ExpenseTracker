using MediatR;

namespace ExpenseTracker.Application.Places.Update;

public class UpdatePlace : IRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
}