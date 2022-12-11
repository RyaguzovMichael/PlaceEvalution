using PlaceEvolution.API.Common.Exceptions;
using PlaceEvolution.API.Common.Extensions;
using PlaceEvolution.API.Domain.Contracts;

namespace PlaceEvolution.API.Domain.Services;

public class PlaceFactory
{
    private readonly IPalceDbService _placeDbService;

    public PlaceFactory(IPalceDbService placeDbService)
    {
        _placeDbService = placeDbService;
    }

    public async Task<Place> CreatePlaceAsync(string title, string description, string photoLink, CancellationToken token)
    {
        title.VerifyStringLength(50, 3);
        description.VerifyStringLength(50, 3);
        photoLink.VerifyStringLength(50, 3);
        if (!await _placeDbService.IsTitleUniqueAsync(title, token))
        {
            throw new ValidationDataException("Title is not unique");
        }

        PlaceIdentity identity = await _placeDbService.GetNewPlaceIdentityAsync(token);

        return Place.Create(identity, title, description, photoLink);
    }

    public async Task<Place> GetPlaceAsync(PlaceIdentity identity, CancellationToken token)
    {
        return await _placeDbService.GetPlaceByIdentityAsync(identity, token);
    }
}
