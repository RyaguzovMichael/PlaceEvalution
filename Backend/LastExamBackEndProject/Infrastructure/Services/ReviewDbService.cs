using LastExamBackEndProject.Common.Exceptions;
using LastExamBackEndProject.Domain;
using LastExamBackEndProject.Domain.Contracts;
using LastExamBackEndProject.Domain.Services;
using LastExamBackEndProject.Infrastructure.Models;
using LastExamBackEndProject.Infrastructure.Models.DomainEntities;
using LastExamBackEndProject.Infrastructure.Repositories;

namespace LastExamBackEndProject.Infrastructure.Services;

public class ReviewDbService : IReviewDbService
{
    private readonly ReviewRepository _reviewRepository;
    private readonly UserFactory _userFactory;

    public ReviewDbService(ReviewRepository reviewRepository, UserFactory userFactory)
    {
        _reviewRepository = reviewRepository;
        _userFactory = userFactory;
    }

    public async Task DeleteReviewAsync(Review review, CancellationToken cancellationToken)
    {
        ReviewDbModel? model = await _reviewRepository.GetByIdAsync(review.Id, cancellationToken);
        if (model is not null)
        {
            await _reviewRepository.DeleteAsync(model, cancellationToken);
        }
    }

    public async Task<ReviewIdentity> GetNewReviewIdentityAsync(PlaceIdentity placeIdentity, UserIdentity userIdentity, CancellationToken cancellationToken)
    {
        ReviewDbModel model = await _reviewRepository.AddAsync(new ReviewDbModel() { PlaceId = placeIdentity.Id, UserId = userIdentity.Id}, cancellationToken);
        return new ReviewIdentity(model.Id);
    }

    public async Task<Review> GetReviewByIdAsync(int id, CancellationToken cancellationToken)
    {
        ReviewDbModel? model = await _reviewRepository.GetByIdAsync(id, cancellationToken);
        if (model is null)
        {
            throw new DatabaseException($"Not finde place with id {id}");
        }
        return await CreateReviewEntity(model, cancellationToken);
    }

    public async Task<Review> GetReviewByIdentityAsync(ReviewIdentity identity, CancellationToken cancellationToken)
    {
        ReviewDbModel? model = await _reviewRepository.GetByIdAsync(identity.Id, cancellationToken);
        if (model is null)
        {
            throw new DatabaseException($"Not finde place with id {identity.Id}");
        }
        return await CreateReviewEntity(model, cancellationToken);
    }

    private async Task<Review> CreateReviewEntity(ReviewDbModel model, CancellationToken cancellationToken)
    {
        UserIdentity identity = await _userFactory.GetUserAsync(model.User.Login, cancellationToken);
        return new ReviewEntity(model.Id, model.Rate, model.ReviewText, identity , model.ReviewDate);
    }
}
