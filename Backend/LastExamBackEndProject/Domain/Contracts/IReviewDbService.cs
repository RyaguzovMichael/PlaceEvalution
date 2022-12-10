namespace LastExamBackEndProject.Domain.Contracts;

public interface IReviewDbService
{
    Task DeleteReviewAsync(Review review, CancellationToken cancellationToken);
    Task<ReviewIdentity> GetNewReviewIdentityAsync(PlaceIdentity placeIdentity, UserIdentity userIdentity, CancellationToken cancellationToken);
    Task<Review> GetReviewByIdAsync(int id, CancellationToken cancellationToken);
    Task<Review> GetReviewByIdentityAsync(ReviewIdentity identity, CancellationToken cancellationToken);
}