using LastExamBackEndProject.Infrastructure.DbContexts;
using LastExamBackEndProject.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace LastExamBackEndProject.Infrastructure.Repositories;

public class ReviewRepository : BaseRepository<ReviewDbModel, ExamDbContext>
{
    public ReviewRepository(ExamDbContext dbContext) : base(dbContext)
    {
    }

    protected override IQueryable<ReviewDbModel> FilterByString(IQueryable<ReviewDbModel> query, string? filterString)
    {
        return filterString is null ? query : query.Where(u => u.ReviewText.Contains(filterString));
    }

    public override async Task<ReviewDbModel?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await Context.Reviews
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
            .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
    }
}
