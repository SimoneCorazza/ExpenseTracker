using MediatR;

namespace ExpenseTracker.Application.Places.Delete;

public class DeletePlace : IRequest
{
    public Guid Id { get; set; }
}