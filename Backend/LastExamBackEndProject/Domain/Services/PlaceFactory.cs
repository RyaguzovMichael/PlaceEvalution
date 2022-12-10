using LastExamBackEndProject.Common.Exceptions;
using LastExamBackEndProject.Common.Extensions;
using LastExamBackEndProject.Domain.Contracts;

namespace LastExamBackEndProject.Domain.Services;

public class PlaceFactory
{
    private readonly IPalceDbService _placeDbService;

    public PlaceFactory(IPalceDbService placeDbService)
    {
        _placeDbService = placeDbService;
    }

    public async Task<Place> CreatePlaceAsync(string title, string description, string photoLink, CancellationToken cancellationToken)
    {
        title.VerifyStringLength(50, 3);
        description.VerifyStringLength(50, 3);
        photoLink.VerifyStringLength(50, 3);
        if (!await _placeDbService.IsTitleUniqueAsync(title, cancellationToken))
        {
            throw new ValidationDataException("Title is not unique");
        }

        PlaceIdentity identity = await _placeDbService.GetNewPlaceIdentityAsync(cancellationToken);

        return Place.Create(identity, title, description, photoLink);
    }

    public async Task<Place> GetPlaceAsync(PlaceIdentity identity, CancellationToken cancellationToken)
    {
        return await _placeDbService.GetPlaceByIdentityAsync(identity, cancellationToken);
    }
}
