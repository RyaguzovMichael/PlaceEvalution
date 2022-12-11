using AutoMapper;
using BaseRepository;
using Microsoft.EntityFrameworkCore;
using PlaceEvolution.API.Common.Exceptions;
using PlaceEvolution.API.Domain;
using PlaceEvolution.API.Domain.Contracts;
using PlaceEvolution.API.Infrastructure.Abstractions;
using PlaceEvolution.API.Infrastructure.Models;
using PlaceEvolution.API.Infrastructure.Models.DbModels;

namespace PlaceEvolution.API.Infrastructure.Services;

public class PlaceDbService : BaseRepository<PlaceDbModel, DataBaseContext>, IPlaceRepository, IPalceDbService
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IMapper _mapper;

    public PlaceDbService(DataBaseContext dbContext, IReviewRepository reviewRepository, IMapper mapper) : base(dbContext)
    {
        _reviewRepository = reviewRepository;
        _mapper = mapper;
    }

    public async Task DeletePlace(PlaceIdentity placeIdentity, CancellationToken token)
    {
        PlaceDbModel? place = await GetByIdAsync(placeIdentity.Id, token);
        if (place is not null)
        {
            await DeleteAsync(place, token);
        }
    }

    public async Task<List<Place>> GetBatchOfPlacesAsync(int pageNumber, int pageSize, string findString, CancellationToken token)
    {
        List<PlaceDbModel> models = await GetFilteredBatchOfData(pageSize, pageNumber, findString, token);
        List<Place> places = new();
        foreach (var place in models)
        {
            places.Add(ConvertDbModelToPlace(place));
        }
        return places;
    }

    public async Task<PlaceIdentity> GetNewPlaceIdentityAsync(CancellationToken token)
    {
        PlaceDbModel model = await AddAsync(new PlaceDbModel(), token);
        return ConvertDbModelToPlace(model);
    }

    public async Task<Place> GetPlaceByIdAsync(int id, CancellationToken token)
    {
        PlaceDbModel? model = await GetByIdAsync(id, token);
        if (model is null)
        {
            throw new DatabaseException($"Not finde place with id {id}");
        }
        return ConvertDbModelToPlace(model);
    }

    public async Task<Place> GetPlaceByIdentityAsync(PlaceIdentity identity, CancellationToken token)
    {
        PlaceDbModel? model = await GetByIdAsync(identity.Id, token);
        if (model is null)
        {
            throw new DatabaseException($"Not finde place with id {identity.Id}");
        }
        return ConvertDbModelToPlace(model);
    }

    public async Task<bool> IsTitleUniqueAsync(string title, CancellationToken token)
    {
        return await GetFirstOrDefaultAsync(p => p.Title == title, token) is null;
    }

    public async Task SavePlace(Place place, CancellationToken token)
    {
        PlaceDbModel? model = await GetByIdAsync(place.Id, token);
        if (model is null)
        {
            throw new DatabaseException($"Not finde place with id {place.Id}");
        }
        model.Title = place.Title;
        model.Rate = place.Rate;
        model.Reviews = _mapper.Map<List<ReviewDbModel>>(place.Reviews);
        model.Description = place.Description;
        model.TitlePhotoLink = place.TitlePhotoLink;
        model.Photos = place.Photos;
        await UpdateAsync(model, token);
    }




    protected override IQueryable<PlaceDbModel> FilterByString(IQueryable<PlaceDbModel> query, string? filterString)
    {
        return filterString is null ? query : query.Where(u => u.Title.Contains(filterString)
                                                            || u.Description.Contains(filterString));
    }

    public async override Task<PlaceDbModel?> GetByIdAsync(int id, CancellationToken token)
    {
        PlaceDbModel? model = await Context.Places.FirstOrDefaultAsync(p => p.Id == id, token);
        if (model is not null)
        {
            model.Reviews = await _reviewRepository.GetReviewsOfPlaceAsync(model.Id, token);
        }
        return model;
    }

    public override async Task<List<PlaceDbModel>> GetFilteredBatchOfData(int pageSize, int page, string? filterString = null, CancellationToken token = default)
    {
        List<PlaceDbModel> places = await base.GetFilteredBatchOfData(pageSize, page, filterString, token);
        foreach (var place in places)
        {
            place.Reviews = await _reviewRepository.GetReviewsOfPlaceAsync(place.Id, token);
        }
        return places;
    }

    public Place ConvertDbModelToPlace(PlaceDbModel model)
    {
        return new PlaceEntity(model.Id,
                               model.Title,
                               model.Rate,
                               model.Description,
                               model.TitlePhotoLink,
                               model.Photos,
                               GetReviewsList(model.Reviews));
    }

    private List<Review> GetReviewsList(ICollection<ReviewDbModel> reviews)
    {
        List<Review> reviewsEntities = new();
        foreach (var review in reviews)
        {
            reviewsEntities.Add(_reviewRepository.ConvertDbModelToReview(review));
        }
        return reviewsEntities;
    }

    private class PlaceEntity : Place
    {
        public PlaceEntity(int id, string title, float rate, string description, string photoLink, List<string> photos, List<Review> reviews)
            : base(id, title, rate, description, photoLink, photos, reviews)
        {
        }
    }
}
