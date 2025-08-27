using ExpenseTracker.Application.Services.User;
using ExpenseTracker.Domain.Places;
using MediatR;

namespace ExpenseTracker.Application.Places.Delete;

public class DeletePlaceHandler : IRequestHandler<DeletePlace>
{
    private readonly IPlaceRepository placeRepository;
    private readonly IUser user;

    public DeletePlaceHandler(IPlaceRepository placeRepository, IUser user)
    {
        this.placeRepository = placeRepository;
        this.user = user;
    }

    public async Task Handle(DeletePlace request, CancellationToken cancellationToken)
    {
        if (user.LoggedUser is null)
        {
            throw new UnauthorizedAccessException("User is not authenticated");
        }

        var place = await placeRepository.GetById(request.Id)
            ?? throw new ArgumentException($"Place with ID {request.Id} not found");

        if (place.UserId != user.LoggedUser.UserId)
        {
            throw new UnauthorizedAccessException("User is not authorized to delete this place");
        }

        placeRepository.Delete(place);
    }
}