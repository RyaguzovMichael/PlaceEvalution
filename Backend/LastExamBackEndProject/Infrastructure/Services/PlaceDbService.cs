using LastExamBackEndProject.Common.Exceptions;
using LastExamBackEndProject.Domain;
using LastExamBackEndProject.Domain.Contracts;
using LastExamBackEndProject.Infrastructure.Models;
using LastExamBackEndProject.Infrastructure.Models.DomainEntities;
using LastExamBackEndProject.Infrastructure.Repositories;

namespace LastExamBackEndProject.Infrastructure.Services;

public class PlaceDbService : IPalceDbService
{
    private readonly PlaceRepository _placeRepository;

    public PlaceDbService(PlaceRepository placeRepository)
    {
        _placeRepository = placeRepository;
    }

    public async Task DeletePlace(PlaceIdentity placeIdentity, CancellationToken cancellationToken)
    {
        PlaceDbModel? place = await _placeRepository.GetByIdAsync(placeIdentity.Id, cancellationToken);
        if (place is not null)
        {
            await _placeRepository.DeleteAsync(place, cancellationToken);
        }
    }

    public async Task<List<Place>> GetBatchOfPlacesAsync(int pageNumber, int pageSize, string findString, CancellationToken cancellationToken)
    {
        List<PlaceDbModel> places = await _placeRepository.GetFilteredBatchOfData(pageSize, pageNumber, findString, cancellationToken);
        List<PlaceEntity> entities = new();
        foreach (var place in places)
        {
            entities.Add(CreatePlaceEntity(place));
        }
        return entities.Select(e => (Place)e).ToList();
    }

    public async Task<PlaceIdentity> GetNewPlaceIdentityAsync(CancellationToken cancellationToken)
    {
        PlaceDbModel model = await _placeRepository.AddAsync(new PlaceDbModel(), cancellationToken);
        return new PlaceIdentity(model.Id);
    }

    public async Task<Place> GetPlaceByIdAsync(int id, CancellationToken cancellationToken)
    {
        PlaceDbModel? model = await _placeRepository.GetByIdAsync(id, cancellationToken);
        if (model is null)
        {
            throw new DatabaseException($"Not finde place with id {id}");
        }
        return CreatePlaceEntity(model);
    }

    public async Task<Place> GetPlaceByIdentityAsync(PlaceIdentity identity, CancellationToken cancellationToken)
    {
        PlaceDbModel? model = await _placeRepository.GetByIdAsync(identity.Id, cancellationToken);
        if (model is null)
        {
            throw new DatabaseException($"Not finde place with id {identity.Id}");
        }
        return CreatePlaceEntity(model);
    }

    public async Task<bool> IsTitleUniqueAsync(string title, CancellationToken cancellationToken)
    {
        return await _placeRepository.GetFirstOrDefaultAsync(p => p.Title == title, cancellationToken) is null;
    }

    public async Task SavePlace(Place place, CancellationToken cancellationToken)
    {
        PlaceDbModel? model = await _placeRepository.GetByIdAsync(place.Id, cancellationToken);
        if (model is null)
        {
            throw new DatabaseException($"Not finde place with id {place.Id}");
        }
        model.Title = place.Title;
        model.Rate = place.Rate;
        model.Reviews = GetReviewDbModelList(place.Reviews ?? new List<Review>());
        model.Description = place.Description;
        model.TitlePhotoLink = place.TitlePhotoLink;
        model.Photos = place.Photos;
        await _placeRepository.UpdateAsync(model, cancellationToken);
    }

    private static PlaceEntity CreatePlaceEntity(PlaceDbModel place)
    {
        return new PlaceEntity(place.Id,
                                        place.Title,
                                        place.Description,
                                        place.TitlePhotoLink,
                                        place.Photos ?? new List<string>(),
                                        GetReviewEntitiesList(place.Reviews ?? new List<ReviewDbModel>()).Select(e => (Review)e).ToList());
    }

    private static List<ReviewEntity> GetReviewEntitiesList(ICollection<ReviewDbModel> reviews)
    {
        List<ReviewEntity> reviewsEntities = new();
        foreach (var review in reviewsEntities)
        {
            reviewsEntities.Add(
                new ReviewEntity(review.Id, review.Rate, review.ReviewText, review.User, review.ReviewDate)
            );
        }

        return reviewsEntities;
    }

    private static List<ReviewDbModel> GetReviewDbModelList(ICollection<Review> reviews)
    {
        List<ReviewDbModel> reviewsEntities = new();
        foreach (var review in reviewsEntities)
        {
            reviewsEntities.Add(new ReviewDbModel()
            {
                Id = review.Id,
                Rate = review.Rate,
                ReviewText = review.ReviewText,
                User = review.User,
                ReviewDate = review.ReviewDate
            });
        }

        return reviewsEntities;
    }

}
