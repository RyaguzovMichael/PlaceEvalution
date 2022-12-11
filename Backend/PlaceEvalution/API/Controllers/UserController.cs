using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlaceEvalution.API.API.Models;
using PlaceEvalution.API.API.Models.Commands;
using PlaceEvalution.API.API.Models.ViewModels;
using PlaceEvalution.API.Common.Extensions;
using PlaceEvalution.API.Domain;
using PlaceEvalution.API.Domain.Services;
using PlaceEvalution.API.Common.Exceptions;
using Swashbuckle.AspNetCore.Annotations;

namespace PlaceEvalution.API.API.Controllers;

[Route("api/[controller]/[action]")]
public class UserController : Controller
{
    private readonly IMapper _mapper;
    private readonly UserService _userService;

    public UserController(IMapper mapper, UserService userService)
    {
        _mapper = mapper;
        _userService = userService;
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Вход пользователя",
        Description = "Для входа необходимо ввести логин и пароль")
    ]
    public async Task<ActionResult<DefaultResponse<CustomerVm>>> Login([FromBody] LoginCommand request, CancellationToken token)
    {
        Customer customer = await _userService.LoginUserAsync(request.Login, request.Password, token);
        HttpContext.Session.SetData(new SessionData { UserIdentity = customer });
        return Ok(new DefaultResponse<CustomerVm>(_mapper.Map<CustomerVm>(customer)));
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Выход пользователя",
        Description = "Для входа нужно просто вызвать этот Endpoint")
    ]
    public ActionResult<DefaultResponse<bool>> Logout()
    {
        HttpContext.Session.SetData(new SessionData { UserIdentity = null });
        return Ok(new DefaultResponse<bool>(true));
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Проверить залогинен ли пользователь в данной сессии",
        Description = "Возвращает bool")
    ]
    public ActionResult<DefaultResponse<bool>> CheckSessionAuthorization()
    {
        SessionData? sessionData = HttpContext.Session.GetData();
        return Ok(new DefaultResponse<bool>(sessionData?.UserIdentity is not null));
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Получить данные о текущем залогиненном пользователе",
        Description = "Если залогинен возвращает UserVm")
    ]
    public async Task<ActionResult<DefaultResponse<CustomerVm>>> GetAuthorizedCustomerData(CancellationToken token)
    {
        SessionData? sessionData = HttpContext.Session.GetData();
        if (sessionData?.UserIdentity is null)
        {
            return Unauthorized();
        }

        Customer customer = await _userService.GetCustomerByIdentityAsync(sessionData.UserIdentity, token);
        return Ok(new DefaultResponse<CustomerVm>(_mapper.Map<CustomerVm>(customer)));
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Регистрация пользователя",
        Description = "Регистрация пользователя на вход получает данные для регистрации")
    ]
    public async Task<ActionResult<DefaultResponse<CustomerVm>>> Register([FromBody] RegisterCommand request, CancellationToken token)
    {
        Customer customer = await _userService.RegisterNewUserAsync(request.Login, request.Password, token);
        customer = await _userService.SetCustomerDataAsync(customer, request.Name, request.Surname, token);
        HttpContext.Session.SetData(new SessionData { UserIdentity = customer });
        return Ok(new DefaultResponse<CustomerVm>(_mapper.Map<CustomerVm>(customer)));
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Смена логина пользователя",
        Description = "Смена логина пользователя на логин переданный в теле запроса")
    ]
    public async Task<ActionResult<DefaultResponse<bool>>> ChangeLogin([FromBody] string login, CancellationToken token)
    {
        SessionData? sessionData = HttpContext.Session.GetData();
        if (sessionData?.UserIdentity is null)
        {
            return Unauthorized();
        }

        User user = await _userService.GetUserByIdentityAsync(sessionData.UserIdentity, token);
        user.ChangeLogin(login);
        await _userService.UpdateUserData(user, token);
        return Ok(new DefaultResponse<bool>(true));
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Смена логина пользователя",
        Description = "Смена логина пользователя на логин переданный в теле запроса")
    ]
    public async Task<ActionResult<DefaultResponse<bool>>> ChangePassword
        ([FromBody] string password, [FromBody] string oldPassword, CancellationToken token)
    {
        SessionData? sessionData = HttpContext.Session.GetData();
        if (sessionData?.UserIdentity is null)
        {
            return Unauthorized();
        }

        User user = await _userService.GetUserByIdentityAsync(sessionData.UserIdentity, token);
        if (!user.CheckPassword(oldPassword))
        {
            throw new ValidationDataException("Old password is not match");
        }
        user.ChangePassword(password);
        await _userService.UpdateUserData(user, token);
        return Ok(new DefaultResponse<bool>(true));
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Смена логина пользователя",
        Description = "Смена логина пользователя на логин переданный в теле запроса")
    ]
    public async Task<ActionResult<DefaultResponse<Customer>>> ChangeUserData
        ([FromBody] string name, [FromBody] string surname, CancellationToken token)
    {
        SessionData? sessionData = HttpContext.Session.GetData();
        if (sessionData?.UserIdentity is null)
        {
            return Unauthorized();
        }
        Customer customer = await _userService.SetCustomerDataAsync(sessionData.UserIdentity, name, surname, token);
        return Ok(new DefaultResponse<CustomerVm>(_mapper.Map<CustomerVm>(customer)));
    }
}