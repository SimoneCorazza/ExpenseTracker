using ExpenseTracker.Application.Get;
using ExpenseTracker.Application.GetPlaces;
using ExpenseTracker.Application.Place.Get;
using ExpenseTracker.Application.Services.User;
using ExpenseTracker.Domain.Places;
using MediatR;

namespace ExpenseTracker.Application.Places.Get;

public class GetPlacesHandler : IRequestHandler<GetPlaces, GetPlacesResponse>
{
    private readonly IPlaceRepository placeRepository;
    private readonly IUser user;

    public GetPlacesHandler(IPlaceRepository placeRepository, IUser user)
    {
        this.placeRepository = placeRepository;
        this.user = user;
    }

    public async Task<GetPlacesResponse> Handle(GetPlaces request, CancellationToken cancellationToken)
    {
        if (user.LoggedUser is null)
        {
            throw new UnauthorizedAccessException("User is not authenticated");
        }

        var places = await placeRepository.GetFor(user.LoggedUser.UserId);
        
        return new GetPlacesResponse
        {
            Places = places.Select(p => new PlaceDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description
            }).ToList()
        };
    }
}