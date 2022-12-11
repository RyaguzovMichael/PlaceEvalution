using BaseRepository.Abstractions;
using PlaceEvalution.API.Domain;
using PlaceEvalution.API.Infrastructure.Models.DbModels;

namespace PlaceEvalution.API.Infrastructure.Abstractions;

public interface IReviewRepository : IBaseRepository<ReviewDbModel>
{
    Review ConvertDbModelToReview(ReviewDbModel model);
    Task<List<ReviewDbModel>> GetReviewsOfPlaceAsync(int placeId, CancellationToken token);
}
