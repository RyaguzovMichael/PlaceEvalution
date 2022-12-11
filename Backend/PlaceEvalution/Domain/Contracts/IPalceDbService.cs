namespace PlaceEvolution.API.Domain.Contracts;

public interface IPalceDbService
{
    Task DeletePlace(PlaceIdentity placeIdentity, CancellationToken token);
    Task<List<Place>> GetBatchOfPlacesAsync(int pageNumber, int pageSize, string findString, CancellationToken token);
    Task<PlaceIdentity> GetNewPlaceIdentityAsync(CancellationToken token);
    Task<Place> GetPlaceByIdAsync(int id, CancellationToken token);
    Task<Place> GetPlaceByIdentityAsync(PlaceIdentity identity, CancellationToken token);
    Task<bool> IsTitleUniqueAsync(string title, CancellationToken token);
    Task SavePlace(Place place, CancellationToken token);
}