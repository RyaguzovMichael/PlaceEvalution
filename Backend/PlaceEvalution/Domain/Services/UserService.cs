using PlaceEvolution.API.Common.Exceptions;
using PlaceEvolution.API.Domain.Contracts;

namespace PlaceEvolution.API.Domain.Services;

public class UserService
{
    private readonly UserFactory _userFactory;
    private readonly IUserDbService _userDbService;

    public UserService(UserFactory userFactory, IUserDbService userDbService)
    {
        _userFactory = userFactory;
        _userDbService = userDbService;
    }

    public async Task<Customer> LoginUserAsync(string login, string password, CancellationToken token)
    {
        User user = await _userFactory.GetUserAsync(login, token);
        if (!user.CheckPassword(password)) throw new ValidationDataException("Login password pair not match");
        return await _userFactory.GetCustomerAsync(user, token);
    }

    public async Task<Customer> GetCustomerByIdentityAsync(UserIdentity userIdentity, CancellationToken token)
    {
        return await _userFactory.GetCustomerAsync(userIdentity, token);
    }

    public async Task<Customer> RegisterNewUserAsync(string login, string password, CancellationToken token)
    {
        User user = await _userFactory.CreateUserAsync(login, password, token);
        await _userDbService.SaveUserDataAsync(user, token);
        return await _userFactory.GetCustomerAsync(user, token);
    }

    public async Task<Customer> SetCustomerDataAsync(UserIdentity identity, string name, string surname, CancellationToken token)
    {
        await _userDbService.UpdateCustomerDataAsync(identity, name, surname, token);
        return await _userFactory.GetCustomerAsync(identity, token);
    }

    public async Task<User> GetUserByIdentityAsync(UserIdentity userIdentity, CancellationToken token)
    {
        return await _userFactory.GetUserAsync(userIdentity, token);
    }

    public async Task UpdateUserData(User user, CancellationToken token)
    {
        await _userDbService.SaveUserDataAsync(user, token);
    }
}