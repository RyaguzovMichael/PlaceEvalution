using PlaceEvolution.API.Common.Exceptions;
using PlaceEvolution.API.Common.Extensions;
using PlaceEvolution.API.Domain.Contracts;

namespace PlaceEvolution.API.Domain.Services;

public class UserFactory
{
    private readonly IUserDbService _userDbService;

    public UserFactory(IUserDbService userDbService)
    {
        _userDbService = userDbService;
    }

    public async Task<User> CreateUserAsync(string login, string password, CancellationToken token)
    {
        login.VerifyStringLength(50, 3);
        password.VerifyStringLength(50, 3);
        if (!await _userDbService.IsLoginUniqueAsync(login, token))
        {
            throw new ValidationDataException("Login is not unique");
        }

        UserIdentity identity = await _userDbService.GetNewUserIdentityAsync(token);

        return User.Create(identity, login, password);
    }

    public async Task<User> GetUserAsync(string login, CancellationToken token)
    {
        return await _userDbService.GetUserByLoginAsync(login, token);
    }

    public async Task<User> GetUserAsync(UserIdentity identity, CancellationToken token)
    {
        return await _userDbService.GetUserByIdentityAsync(identity, token);
    }

    public async Task<Customer> GetCustomerAsync(UserIdentity identity, CancellationToken token)
    {
        return await _userDbService.GetCustomerByIdentityAsync(identity, token);
    }

    internal Customer GetCustomer(UserIdentity identity)
    {
        return _userDbService.GetCustomerByIdentity(identity);
    }
}