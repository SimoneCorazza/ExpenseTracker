using ExpenseTracker.Application.Services.User;
using ExpenseTracker.Domain.Places;
using MediatR;

namespace ExpenseTracker.Application.Places.Edit;

public class EditPlaceHandler : IRequestHandler<EditPlace>
{
    private readonly IPlaceRepository placeRepository;
    private readonly IUser user;

    public EditPlaceHandler(IPlaceRepository placeRepository, IUser user)
    {
        this.placeRepository = placeRepository;
        this.user = user;
    }

    public async Task Handle(EditPlace request, CancellationToken cancellationToken)
    {
        if (user.LoggedUser is null)
        {
            throw new UnauthorizedAccessException("User is not authenticated");
        }

        var place = await placeRepository.GetById(request.Id)
            ?? throw new ArgumentException($"Place with ID {request.Id} not found");

        if (place.UserId != user.LoggedUser.UserId)
        {
            throw new UnauthorizedAccessException("User is not authorized to update this place");
        }

        place.Update(request.Name, request.Description);
        await placeRepository.Update(place);
    }
}