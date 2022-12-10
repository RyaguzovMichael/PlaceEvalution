namespace LastExamBackEndProject.Domain.Contracts;

public interface IPalceDbService
{
    Task DeletePlace(PlaceIdentity placeIdentity, CancellationToken cancellationToken);
    Task<List<Place>> GetBatchOfPlacesAsync(int pageNumber, int pageSize, string findString, CancellationToken cancellationToken);
    Task<PlaceIdentity> GetNewPlaceIdentityAsync(CancellationToken cancellationToken);
    Task<Place> GetPlaceByIdAsync(int id, CancellationToken cancellationToken);
    Task<Place> GetPlaceByIdentityAsync(PlaceIdentity identity, CancellationToken cancellationToken);
    Task<bool> IsTitleUniqueAsync(string title, CancellationToken cancellationToken);
    Task SavePlace(Place place, CancellationToken cancellationToken);
}