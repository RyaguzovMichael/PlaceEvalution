using BaseRepository.Abstractions;
using LastExamBackEndProject.Domain;
using LastExamBackEndProject.Infrastructure.Models.DbModels;

namespace LastExamBackEndProject.Infrastructure.Abstractions;

public interface IReviewRepository : IBaseRepository<ReviewDbModel>
{
    Review ConvertDbModelToReview(ReviewDbModel model);
    Task<List<ReviewDbModel>> GetReviewsOfPlaceAsync(int placeId, CancellationToken token);
}
