namespace LastExamBackEndProject.Domain.Contracts;

public interface IReviewDbService
{
    Task DeleteReviewAsync(Review review, CancellationToken token);
    Task<ReviewIdentity> GetNewReviewIdentityAsync(PlaceIdentity placeIdentity, UserIdentity userIdentity, CancellationToken token);
    Task<Review> GetReviewByIdAsync(int id, CancellationToken token);
    Task<Review> GetReviewByIdentityAsync(ReviewIdentity identity, CancellationToken token);
}