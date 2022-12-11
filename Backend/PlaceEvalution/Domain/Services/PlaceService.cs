using PlaceEvalution.API.Domain.Contracts;
using PlaceEvalution.API.Common.Exceptions;

namespace PlaceEvalution.API.Domain.Services;

public class PlaceService
{
    private readonly PlaceFactory _placeFactory;
    private readonly ReviewFactory _reviewFactory;
    private readonly IPalceDbService _placeDbService;
    private readonly IReviewDbService _reviewDbService;

    public PlaceService(PlaceFactory placeFactory,
                        ReviewFactory reviewFactory,
                        IPalceDbService placeDbService,
                        IReviewDbService reviewDbService)
    {
        _placeFactory = placeFactory;
        _reviewFactory = reviewFactory;
        _placeDbService = placeDbService;
        _reviewDbService = reviewDbService;
    }

    public async Task<Place> CreatePlaceAsync(string title, string description, string photoLink, CancellationToken token)
    {
        return await _placeFactory.CreatePlaceAsync(title, description, photoLink, token);
    }

    public async Task<Place> GetPlaceByIdAsync(int id, CancellationToken token)
    {
        return await _placeDbService.GetPlaceByIdAsync(id, token);
    }

    public async Task<Place> GetPlaceByIdentityAsync(PlaceIdentity identity, CancellationToken token)
    {
        return await _placeDbService.GetPlaceByIdentityAsync(identity, token);
    }

    public async Task<Place> AddRewiewToPlaceAsync
        (int rate, string reviewText, UserIdentity userIdentity, PlaceIdentity placeIdentity, CancellationToken token)
    {
        Place place = await _placeFactory.GetPlaceAsync(placeIdentity, token);
        Review review = await _reviewFactory.CreateReviewAsync(rate, reviewText, placeIdentity, userIdentity, token);
        place.AddReview(review);
        return place;
    }

    public async Task<Place> RemoveReviewFromPlaceAsync
        (ReviewIdentity reviewIdentity, PlaceIdentity placeIdentity, UserIdentity userIdentity, CancellationToken token)
    {
        Place place = await _placeFactory.GetPlaceAsync(placeIdentity, token);
        Review review = await _reviewFactory.GetReviewAsync(reviewIdentity, token);
        place.DeleteReview(review, userIdentity);
        await _reviewDbService.DeleteReviewAsync(review, token);
        return place;
    }

    public async Task<Place> AddPhotoToPlaceAsync
        (string photoLink, PlaceIdentity placeIdentity, CancellationToken token)
    {
        Place place = await _placeFactory.GetPlaceAsync(placeIdentity, token);
        place.AddPhoto(photoLink);
        return place;
    }

    public async Task<Place> RemovePhotoFromPlaceAsync
        (string photoLink, PlaceIdentity placeIdentity, UserIdentity userIdentity, CancellationToken token)
    {
        Place place = await _placeFactory.GetPlaceAsync(placeIdentity, token);
        place.DeletePhoto(photoLink, userIdentity);
        return place;
    }

    public async Task DeletePlaceAsync(PlaceIdentity placeIdentity, UserIdentity userIdentity, CancellationToken token)
    {
        if (userIdentity.Role == UserRoles.Admin)
        {
            await _placeDbService.DeletePlace(placeIdentity, token);
        }
        else
        {
            throw new UserAccessException($"User with id : {userIdentity.Id}, don't have permissions to delete place");
        }
    }
}
