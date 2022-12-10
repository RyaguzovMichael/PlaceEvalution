using LastExamBackEndProject.Infrastructure.DbContexts;
using LastExamBackEndProject.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace LastExamBackEndProject.Infrastructure.Repositories;

public class PlaceRepository : BaseRepository<PlaceDbModel, ExamDbContext>
{
    public PlaceRepository(ExamDbContext dbContext) : base(dbContext)
    {
    }

    protected override IQueryable<PlaceDbModel> FilterByString(IQueryable<PlaceDbModel> query, string? filterString)
    {
        return filterString is null ? query : query.Where(u => u.Title.Contains(filterString) || u.Description.Contains(filterString));
    }

    public async override Task<PlaceDbModel?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        PlaceDbModel? model = await Context.Places.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        if (model is not null)
        {
            model.Reviews = await GetReviewDbModels(model, cancellationToken);
        }
        return model;
    }

    public override async Task<List<PlaceDbModel>> GetFilteredBatchOfData(int pageSize, int page, string? filterString = null, CancellationToken cancellationToken = default)
    {
        List<PlaceDbModel> places = await base.GetFilteredBatchOfData(pageSize, page, filterString, cancellationToken);
        foreach (var place in places)
        {
            place.Reviews = await GetReviewDbModels(place, cancellationToken);
        }
        return places;
    }

    private async Task<List<ReviewDbModel>> GetReviewDbModels(PlaceDbModel model, CancellationToken cancellationToken)
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
                        .Where(r => r.PlaceId == model.Id)
                        .ToListAsync(cancellationToken);
    }
}
