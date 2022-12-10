using LastExamBackEndProject.Common.Exceptions;
using LastExamBackEndProject.Domain.Contracts;

namespace LastExamBackEndProject.Domain.Services;

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

    public async Task<Place> CreatePlaceAsync(string title, string description, string photoLink, CancellationToken cancellationToken)
    {
        return await _placeFactory.CreatePlaceAsync(title, description, photoLink, cancellationToken); 
    }

    public async Task<Place> GetPlaceByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _placeDbService.GetPlaceByIdAsync(id, cancellationToken);
    }

    public async Task<Place> GetPlaceByIdentityAsync(PlaceIdentity identity, CancellationToken cancellationToken)
    {
        return await _placeDbService.GetPlaceByIdentityAsync(identity, cancellationToken);
    }

    public async Task<Place> AddRewiewToPlaceAsync
        (int rate, string reviewText, UserIdentity userIdentity, PlaceIdentity placeIdentity, CancellationToken cancellationToken)
    {
        Place place = await _placeFactory.GetPlaceAsync(placeIdentity, cancellationToken);
        Review review = await _reviewFactory.CreateReviewAsync(rate, reviewText, placeIdentity, userIdentity, cancellationToken);
        place.AddReview(review);
        return place;
    }

    public async Task<Place> RemoveReviewFromPlaceAsync
        (ReviewIdentity reviewIdentity, PlaceIdentity placeIdentity, UserIdentity userIdentity, CancellationToken cancellationToken)
    {
        Place place = await _placeFactory.GetPlaceAsync(placeIdentity, cancellationToken);
        Review review = await _reviewFactory.GetReviewAsync(reviewIdentity, cancellationToken);
        place.DeleteReview(review, userIdentity);
        await _reviewDbService.DeleteReviewAsync(review, cancellationToken);
        return place;
    }

    public async Task<Place> AddPhotoToPlaceAsync
        (string photoLink, PlaceIdentity placeIdentity, CancellationToken cancellationToken)
    {
        Place place = await _placeFactory.GetPlaceAsync(placeIdentity, cancellationToken);
        place.AddPhoto(photoLink);
        return place;
    }

    public async Task<Place> RemovePhotoFromPlaceAsync
        (string photoLink, PlaceIdentity placeIdentity, UserIdentity userIdentity, CancellationToken cancellationToken)
    {
        Place place = await _placeFactory.GetPlaceAsync(placeIdentity, cancellationToken);
        place.DeletePhoto(photoLink, userIdentity);
        return place;
    }

    public async Task DeletePlaceAsync(PlaceIdentity placeIdentity, UserIdentity userIdentity, CancellationToken cancellationToken)
    {
        if (userIdentity.Role == UserRoles.Admin)
        {
            await _placeDbService.DeletePlace(placeIdentity, cancellationToken);
        }
        else
        {
            throw new UserAccessException($"User with id : {userIdentity.Id}, don't have permissions to delete place");
        }
    }
}
