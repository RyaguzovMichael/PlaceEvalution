namespace LastExamBackEndProject.Domain.Contracts;

public interface IReviewDbService
{
    Task<ReviewIdentity> GetNewReviewIdentityAsync(CancellationToken cancellationToken);
    Task<Review> GetReviewByIdAsync(int id, CancellationToken cancellationToken);
    Task<Review> GetReviewByIdentityAsync(ReviewIdentity identity, CancellationToken cancellationToken);
}