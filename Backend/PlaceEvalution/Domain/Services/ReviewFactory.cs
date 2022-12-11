using PlaceEvolution.API.Common.Extensions;
using PlaceEvolution.API.Domain.Contracts;

namespace PlaceEvolution.API.Domain.Services;

public class ReviewFactory
{
    private readonly IReviewDbService _reviewDbService;

    public ReviewFactory(IReviewDbService reviewDbService)
    {
        _reviewDbService = reviewDbService;
    }

    public async Task<Review> CreateReviewAsync
        (int rate, string reviewText, PlaceIdentity placeIdentity, UserIdentity userIdentity, CancellationToken token)
    {
        reviewText.VerifyStringLength(300, 3);

        ReviewIdentity identity = await _reviewDbService.GetNewReviewIdentityAsync(placeIdentity, userIdentity, token);

        return Review.Create(identity, rate, reviewText, userIdentity);
    }

    public async Task<Review> GetReviewAsync(ReviewIdentity identity, CancellationToken token)
    {
        return await _reviewDbService.GetReviewByIdentityAsync(identity, token);
    }

    public async Task<Review> GetReviewAsync(int id, CancellationToken token)
    {
        return await _reviewDbService.GetReviewByIdAsync(id, token);
    }
}
