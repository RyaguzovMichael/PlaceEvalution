using BaseRepository;
using LastExamBackEndProject.Common.Exceptions;
using LastExamBackEndProject.Domain;
using LastExamBackEndProject.Domain.Contracts;
using LastExamBackEndProject.Infrastructure.Abstractions;
using LastExamBackEndProject.Infrastructure.Models;
using LastExamBackEndProject.Infrastructure.Models.DbModels;
using Microsoft.EntityFrameworkCore;

namespace LastExamBackEndProject.Infrastructure.Services;

public class ReviewDbService : BaseRepository<ReviewDbModel, DataBaseContext>, IReviewRepository, IReviewDbService
{
    private readonly IUserRepository _userRepository;

    public ReviewDbService(DataBaseContext dbContext, IUserRepository userRepository) : base(dbContext)
    {
        _userRepository = userRepository;
    }

    public async Task DeleteReviewAsync(Review review, CancellationToken token)
    {
        ReviewDbModel? model = await GetByIdAsync(review.Id, token);
        if (model is not null)
        {
            await DeleteAsync(model, token);
        }
    }

    public async Task<ReviewIdentity> GetNewReviewIdentityAsync(PlaceIdentity placeIdentity, UserIdentity userIdentity, CancellationToken token)
    {
        ReviewDbModel model = await AddAsync(new ReviewDbModel() { PlaceId = placeIdentity.Id, UserId = userIdentity.Id }, token);
        return ConvertDbModelToReview(model);
    }

    public async Task<Review> GetReviewByIdAsync(int id, CancellationToken token)
    {
        ReviewDbModel? model = await GetByIdAsync(id, token);
        if (model is null)
        {
            throw new DatabaseException($"Not finde place with id {id}");
        }
        return ConvertDbModelToReview(model);
    }

    public async Task<Review> GetReviewByIdentityAsync(ReviewIdentity identity, CancellationToken token)
    {
        ReviewDbModel? model = await GetByIdAsync(identity.Id, token);
        if (model is null)
        {
            throw new DatabaseException($"Not finde place with id {identity.Id}");
        }
        return ConvertDbModelToReview(model);
    }

    protected override IQueryable<ReviewDbModel> FilterByString(IQueryable<ReviewDbModel> query, string? filterString)
    {
        return filterString is null ? query : query.Where(u => u.ReviewText.Contains(filterString));
    }

    public override async Task<ReviewDbModel?> GetByIdAsync(int id, CancellationToken token)
    {
        return await DbSet
                        .Join(Context.Users, r => r.UserId, u => u.Id, (inner, result) => new ReviewDbModel
                        {
                            Id = inner.Id,
                            ReviewText = inner.ReviewText,
                            User = result,
                            UserId = inner.UserId,
                            CreatedDate = inner.CreatedDate,
                            LastModifiedDate = inner.LastModifiedDate,
                            PlaceId = inner.PlaceId,
                            Rate = inner.Rate,
                            ReviewDate = inner.ReviewDate
                        })
                        .Join(Context.Places, r => r.PlaceId, u => u.Id, (inner, result) => new ReviewDbModel
                        {
                            Id = inner.Id,
                            ReviewText = inner.ReviewText,
                            User = inner.User,
                            UserId = inner.UserId,
                            CreatedDate = inner.CreatedDate,
                            LastModifiedDate = inner.LastModifiedDate,
                            PlaceId = inner.PlaceId,
                            Place = result,
                            Rate = inner.Rate,
                            ReviewDate = inner.ReviewDate
                        })
                        .FirstOrDefaultAsync(r => r.Id == id, token);
    }

    public async Task<List<ReviewDbModel>> GetReviewsOfPlaceAsync(int placeId, CancellationToken token)
    {
        return await DbSet.Join(Context.Users, r => r.UserId, u => u.Id, (inner, result) => new ReviewDbModel
                            {
                                Id = inner.Id,
                                ReviewText = inner.ReviewText,
                                User = result,
                                UserId = inner.UserId,
                                CreatedDate = inner.CreatedDate,
                                LastModifiedDate = inner.LastModifiedDate,
                                PlaceId = inner.PlaceId,
                                Rate = inner.Rate,
                                ReviewDate = inner.ReviewDate
                            })
                            .Where(r => r.PlaceId == placeId)
                            .ToListAsync(token);
    }

    public Review ConvertDbModelToReview(ReviewDbModel model)
    {
        Customer customer = _userRepository.ConvertDbModelToCustomer(model.User);
        return new ReviewEntity(model.Id, model.Rate, model.ReviewText, customer, model.ReviewDate);
    }

    private class ReviewEntity : Review
    {
        public ReviewEntity(int id, int rate, string reviewText, UserIdentity user, DateTime reviewDate)
            : base(id, rate, reviewText, user, reviewDate)
        {
        }
    }

}
