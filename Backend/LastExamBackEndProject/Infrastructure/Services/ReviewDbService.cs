using LastExamBackEndProject.Common.Exceptions;
using LastExamBackEndProject.Domain;
using LastExamBackEndProject.Domain.Contracts;
using LastExamBackEndProject.Infrastructure.Models;
using LastExamBackEndProject.Infrastructure.Models.DomainEntities;
using LastExamBackEndProject.Infrastructure.Repositories;

namespace LastExamBackEndProject.Infrastructure.Services;

public class ReviewDbService : IReviewDbService
{
    private readonly ReviewRepository _reviewRepository;

    public ReviewDbService(ReviewRepository reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }

    public async Task<ReviewIdentity> GetNewReviewIdentityAsync(CancellationToken cancellationToken)
    {
        ReviewDbModel model = await _reviewRepository.AddAsync(new ReviewDbModel(), cancellationToken);
        return new ReviewIdentity(model.Id);
    }

    public async Task<Review> GetReviewByIdAsync(int id, CancellationToken cancellationToken)
    {
        ReviewDbModel? model = await _reviewRepository.GetByIdAsync(id, cancellationToken);
        if (model is null)
        {
            throw new DatabaseException($"Not finde place with id {id}");
        }
        return CreateReviewEntity(model);
    }

    public async Task<Review> GetReviewByIdentityAsync(ReviewIdentity identity, CancellationToken cancellationToken)
    {
        ReviewDbModel? model = await _reviewRepository.GetByIdAsync(identity.Id, cancellationToken);
        if (model is null)
        {
            throw new DatabaseException($"Not finde place with id {identity.Id}");
        }
        return CreateReviewEntity(model);
    }

    private Review CreateReviewEntity(ReviewDbModel model)
    {
        return new ReviewEntity(model.Id, model.Rate, model.ReviewText, new UserIdentity(model.UserId), model.ReviewDate);
    }
}
