using AutoMapper;
using LastExamBackEndProject.API.Contracts;
using LastExamBackEndProject.API.Models;
using LastExamBackEndProject.API.Models.Commands;
using LastExamBackEndProject.API.Models.ViewModels;
using LastExamBackEndProject.Common.Exceptions;
using LastExamBackEndProject.Common.Extensions;
using LastExamBackEndProject.Domain;
using LastExamBackEndProject.Domain.Contracts;
using LastExamBackEndProject.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LastExamBackEndProject.API.Controllers;

[Route("[controller]/[action]")]
public class PlaceController : Controller
{
    private readonly IMapper _mapper;
    private readonly PlaceService _placeService;
    private readonly ReviewFactory _reviewFactory;
    private readonly IFileDbService _fileDbService;
    private readonly IPalceDbService _placeDbService;

    public PlaceController(IMapper mapper,
                           PlaceService placeService,
                           IFileDbService fileDbService,
                           IPalceDbService palceDbService,
                           ReviewFactory reviewFactory)
    {
        _mapper = mapper;
        _placeService = placeService;
        _fileDbService = fileDbService;
        _placeDbService = palceDbService;
        _reviewFactory = reviewFactory;
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Создание заведения",
        Description = "Для создания необходимо ввести название заведения описание заведения и передать файл с фотографией")
    ]
    public async Task<ActionResult<DefaultResponse<bool>>> Create([FromForm] CreatePlaceCommand request, CancellationToken cancellationToken)
    {
        SessionData? sessionData = HttpContext.Session.GetData();
        if (sessionData?.UserIdentity is null)
        {
            throw new UserAuthorizeException("Non authorized user try create Place");
        }
        string photoLink = await _fileDbService.SaveFileAsync(request.File, cancellationToken);
        Place place = await _placeService.CreatePlaceAsync(request.Title, request.Description, photoLink, cancellationToken);
        await _placeDbService.SavePlace(place, cancellationToken);
        return Ok(new DefaultResponse<bool>(true));
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Получение данных о заведении",
        Description = "Для получения необходимо передать id заведения")
    ]
    public async Task<ActionResult<DefaultResponse<PlaceVm>>> Get([FromBody] int id, CancellationToken cancellationToken)
    {
        Place place = await _placeService.GetPlaceByIdAsync(id, cancellationToken);
        return Ok(_mapper.Map<PlaceVm>(place));
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Получение списка заведений с пагинацией и поисковой строкой",
        Description = "Для получения необходимо передать номер страницы, и поисковую строку")
    ]
    public async Task<ActionResult<DefaultResponse<List<PlaceShortVm>>>> GetBatch
        ([FromQuery] int pageNumber, [FromQuery] string findString, CancellationToken cancellationToken)
    {
        const int pageSize = 12;
        List<Place> places = await _placeDbService.GetBatchOfPlacesAsync(pageNumber, pageSize, findString, cancellationToken);
        return Ok(_mapper.Map<List<PlaceShortVm>>(places));
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Удаление заведения",
        Description = "Для удаления необходимо ввести Id заведения")
    ]
    public async Task<ActionResult<DefaultResponse<bool>>> DeletePlace([FromBody] DeletePlaceCommand request, CancellationToken cancellationToken)
    {
        SessionData? sessionData = HttpContext.Session.GetData();
        if (sessionData?.UserIdentity is null)
        {
            throw new UserAuthorizeException($"Non authorized user try delete place with id {request.PlaceId}");
        }
        PlaceIdentity? placeIdentity = await _placeService.GetPlaceByIdAsync(request.PlaceId, cancellationToken);
        await _placeService.DeletePlaceAsync(placeIdentity, sessionData.UserIdentity, cancellationToken);
        return Ok(new DefaultResponse<bool>(true));
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Добавление отзыва для заведения",
        Description = "Для добавления необходимо ввести Id заведения, оценку и описание отзыва")
    ]
    public async Task<ActionResult<DefaultResponse<PlaceVm>>> AddReview([FromBody] AddReviewCommand request, CancellationToken cancellationToken)
    {
        SessionData? sessionData = HttpContext.Session.GetData();
        if (sessionData?.UserIdentity is null)
        {
            throw new UserAuthorizeException($"Non authorized user try add review to place with id {request.PlaceId}");
        }
        PlaceIdentity? placeIdentity = await _placeService.GetPlaceByIdAsync(request.PlaceId, cancellationToken);
        Place place = await _placeService.AddRewiewToPlaceAsync(request.Score, request.ReviewText, sessionData.UserIdentity, placeIdentity, cancellationToken);
        await _placeDbService.SavePlace(place, cancellationToken);
        return Ok(_mapper.Map<PlaceVm>(place));
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Удаление отзыва",
        Description = "Для удаления необходимо ввести Id заведения, и Id отзыва")
    ]
    public async Task<ActionResult<DefaultResponse<PlaceVm>>> RemoveReview([FromBody] RemoveReviewCommand request, CancellationToken cancellationToken)
    {
        SessionData? sessionData = HttpContext.Session.GetData();
        if (sessionData?.UserIdentity is null)
        {
            throw new UserAuthorizeException($"Non authorized user try remove review with id {request.ReviewId} by place with id {request.PlaceId}");
        }
        PlaceIdentity? placeIdentity = await _placeService.GetPlaceByIdAsync(request.PlaceId, cancellationToken);
        ReviewIdentity? reviewIdentity = await _reviewFactory.GetReviewAsync(request.ReviewId, cancellationToken);
        Place place = await _placeService.RemoveReviewFromPlaceAsync(reviewIdentity, placeIdentity, sessionData.UserIdentity ,cancellationToken);
        await _placeDbService.SavePlace(place, cancellationToken);
        return Ok(_mapper.Map<PlaceVm>(place));
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Добавление фото для заведения",
        Description = "Для добавления необходимо ввести Id заведения, и загрузить файл с фото")
    ]
    public async Task<ActionResult<DefaultResponse<PlaceVm>>> AddPhoto([FromForm] AddPhotoCommand request, CancellationToken cancellationToken)
    {
        SessionData? sessionData = HttpContext.Session.GetData();
        if (sessionData?.UserIdentity is null)
        {
            throw new UserAuthorizeException($"Non authorized user try add photo to place with id {request.PlaceId}");
        }
        PlaceIdentity? placeIdentity = await _placeService.GetPlaceByIdAsync(request.PlaceId, cancellationToken);
        string photoLink = await _fileDbService.SaveFileAsync(request.Photo, cancellationToken);
        Place place = await _placeService.AddPhotoToPlaceAsync(photoLink, placeIdentity ,cancellationToken);
        await _placeDbService.SavePlace(place, cancellationToken);
        return Ok(_mapper.Map<PlaceVm>(place));
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Удаление фото для заведения",
        Description = "Для удаления необходимо ввести Id заведения, и передать путь к файлу с фото")
    ]
    public async Task<ActionResult<DefaultResponse<PlaceVm>>> RemovePhoto([FromBody] RemovePhotoCommand request, CancellationToken cancellationToken)
    {
        SessionData? sessionData = HttpContext.Session.GetData();
        if (sessionData?.UserIdentity is null)
        {
            throw new UserAuthorizeException($"Non authorized user try remove photo to place with id {request.PlaceId}");
        }
        PlaceIdentity? placeIdentity = await _placeService.GetPlaceByIdAsync(request.PlaceId, cancellationToken);
        await _fileDbService.DeleteFileAsync(request.PhotoLink, cancellationToken);
        Place place = await _placeService.RemovePhotoFromPlaceAsync(request.PhotoLink, placeIdentity, sessionData.UserIdentity, cancellationToken);
        await _placeDbService.SavePlace(place, cancellationToken);
        return Ok(_mapper.Map<PlaceVm>(place));
    }
}
