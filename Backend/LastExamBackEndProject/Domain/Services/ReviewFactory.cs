using LastExamBackEndProject.Common.Extensions;
using LastExamBackEndProject.Domain.Contracts;

namespace LastExamBackEndProject.Domain.Services;

public class ReviewFactory
{
    private readonly IReviewDbService _reviewDbService;

    public ReviewFactory(IReviewDbService reviewDbService)
    {
        _reviewDbService = reviewDbService;
    }

    public async Task<Review> CreateReviewAsync
        (int rate, string reviewText, UserIdentity userIdentity, CancellationToken cancellationToken)
    {
        reviewText.VerifyStringLength(300, 3);

        ReviewIdentity identity = await _reviewDbService.GetNewReviewIdentityAsync(cancellationToken);

        return Review.Create(identity, rate, reviewText, userIdentity);
    }

    public async Task<Review> GetReviewAsync(ReviewIdentity identity, CancellationToken cancellationToken)
    {
        return await _reviewDbService.GetReviewByIdentityAsync(identity, cancellationToken);
    }

    public async Task<Review> GetReviewAsync(int id, CancellationToken cancellationToken)
    {
        return await _reviewDbService.GetReviewByIdAsync(id, cancellationToken);
    }
}
