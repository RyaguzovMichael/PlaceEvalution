using BaseRepository.Abstractions;
using PlaceEvolution.API.Domain;
using PlaceEvolution.API.Infrastructure.Models.DbModels;

namespace PlaceEvolution.API.Infrastructure.Abstractions;

public interface IReviewRepository : IBaseRepository<ReviewDbModel>
{
    Review ConvertDbModelToReview(ReviewDbModel model);
    Task<List<ReviewDbModel>> GetReviewsOfPlaceAsync(int placeId, CancellationToken token);
}
