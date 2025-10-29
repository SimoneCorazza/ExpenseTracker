using ExpenseTracker.Application.Services.User;
using ExpenseTracker.Domain.Places;
using MediatR;

namespace ExpenseTracker.Application.Places.Create;

public class CreatePlaceHandler : IRequestHandler<CreatePlace, CreatePlaceResponse>
{
    private readonly IPlaceRepository placeRepository;
    private readonly IUser user;

    public CreatePlaceHandler(IPlaceRepository placeRepository, IUser user)
    {
        this.placeRepository = placeRepository;
        this.user = user;
    }

    public async Task<CreatePlaceResponse> Handle(CreatePlace request, CancellationToken cancellationToken)
    {
        if (user.LoggedUser is null)
        {
            throw new UnauthorizedAccessException("User is not authenticated");
        }

        var place = new Place(request.Name, request.Description, user.LoggedUser.UserId);
        await placeRepository.Add(place);

        return new CreatePlaceResponse
        {
            Id = place.Id,
        };
    }
}