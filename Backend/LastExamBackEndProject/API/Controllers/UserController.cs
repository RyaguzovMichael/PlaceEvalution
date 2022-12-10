﻿using AutoMapper;
using LastExamBackEndProject.API.Models;
using LastExamBackEndProject.API.Models.Commands;
using LastExamBackEndProject.API.Models.ViewModels;
using LastExamBackEndProject.Common.Exceptions;
using LastExamBackEndProject.Common.Extensions;
using LastExamBackEndProject.Domain;
using LastExamBackEndProject.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LastExamBackEndProject.API.Controllers;

[Route("[controller]/[action]")]
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
    public async Task<ActionResult<DefaultResponse<CustomerVm>>> Login([FromBody] LoginCommand request, CancellationToken cancellationToken)
    {
        Customer customer = await _userService.LoginUserAsync(request.Login, request.Password, cancellationToken);
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
    public async Task<ActionResult<DefaultResponse<CustomerVm>>> GetAuthorizedCustomerData(CancellationToken cancellationToken)
    {
        SessionData? sessionData = HttpContext.Session.GetData();
        if (sessionData?.UserIdentity is null)
        {
            return Unauthorized();
        }

        Customer customer = await _userService.GetCustomerByIdentityAsync(sessionData.UserIdentity, cancellationToken);
        return Ok(new DefaultResponse<CustomerVm>(_mapper.Map<CustomerVm>(customer)));
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Регистрация пользователя",
        Description = "Регистрация пользователя на вход получает данные для регистрации")
    ]
    public async Task<ActionResult<DefaultResponse<CustomerVm>>> Register([FromBody] RegisterCommand request, CancellationToken cancellationToken)
    {
        Customer customer = await _userService.RegisterNewUserAsync(request.Login, request.Password, cancellationToken);
        customer = await _userService.SetCustomerDataAsync(customer, request.Name, request.Surname, cancellationToken);
        HttpContext.Session.SetData(new SessionData { UserIdentity = customer });
        return Ok(new DefaultResponse<CustomerVm>(_mapper.Map<CustomerVm>(customer)));
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Смена логина пользователя",
        Description = "Смена логина пользователя на логин переданный в теле запроса")
    ]
    public async Task<ActionResult<DefaultResponse<bool>>> ChangeLogin([FromBody] string login, CancellationToken cancellationToken)
    {
        SessionData? sessionData = HttpContext.Session.GetData();
        if (sessionData?.UserIdentity is null)
        {
            return Unauthorized();
        }

        User user = await _userService.GetUserByIdentityAsync(sessionData.UserIdentity, cancellationToken);
        user.ChangeLogin(login);
        await _userService.UpdateUserData(user, cancellationToken);
        return Ok(new DefaultResponse<bool>(true));
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Смена логина пользователя",
        Description = "Смена логина пользователя на логин переданный в теле запроса")
    ]
    public async Task<ActionResult<DefaultResponse<bool>>> ChangePassword
        ([FromBody] string password, [FromBody] string oldPassword, CancellationToken cancellationToken)
    {
        SessionData? sessionData = HttpContext.Session.GetData();
        if (sessionData?.UserIdentity is null)
        {
            return Unauthorized();
        }

        User user = await _userService.GetUserByIdentityAsync(sessionData.UserIdentity, cancellationToken);
        if (!user.CheckPassword(oldPassword))
        {
            throw new ValidationDataException("Old password is not match");
        }
        user.ChangePassword(password);
        await _userService.UpdateUserData(user, cancellationToken);
        return Ok(new DefaultResponse<bool>(true));
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Смена логина пользователя",
        Description = "Смена логина пользователя на логин переданный в теле запроса")
    ]
    public async Task<ActionResult<DefaultResponse<Customer>>> ChangeUserData
        ([FromBody] string name, [FromBody] string surname, CancellationToken cancellationToken)
    {
        SessionData? sessionData = HttpContext.Session.GetData();
        if (sessionData?.UserIdentity is null)
        {
            return Unauthorized();
        }
        Customer customer = await _userService.SetCustomerDataAsync(sessionData.UserIdentity, name, surname, cancellationToken);
        return Ok(new DefaultResponse<CustomerVm>(_mapper.Map<CustomerVm>(customer)));
    }
}