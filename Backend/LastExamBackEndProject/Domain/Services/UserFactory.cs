using LastExamBackEndProject.Common.Exceptions;
using LastExamBackEndProject.Common.Extensions;
using LastExamBackEndProject.Domain.Contracts;
using System.Security.Principal;
using System.Threading;

namespace LastExamBackEndProject.Domain.Services;

public class UserFactory
{
    private readonly IUserDbService _userDbService;

    public UserFactory(IUserDbService userDbService)
    {
        _userDbService = userDbService;
    }

    public async Task<User> CreateUserAsync(string login, string password, CancellationToken cancellationToken)
    {
        login.VerifyStringLength(50, 3);
        password.VerifyStringLength(50, 3);
        if (!await _userDbService.IsLoginUniqueAsync(login, cancellationToken))
        {
            throw new ValidationDataException("Login is not unique");
        }

        UserIdentity identity = await _userDbService.GetNewUserIdentityAsync(cancellationToken);

        return User.Create(identity, login, password);
    }

    public async Task<User> GetUserAsync(string login, CancellationToken cancellationToken)
    {
        return await _userDbService.GetUserByLoginAsync(login, cancellationToken);
    }

    public async Task<User> GetUserAsync(UserIdentity identity, CancellationToken cancellationToken)
    {
        return await _userDbService.GetUserByIdentityAsync(identity, cancellationToken);
    }

    public async Task<Customer> GetCustomerAsync(UserIdentity identity, CancellationToken cancellationToken)
    {
        return await _userDbService.GetCustomerByIdentityAsync(identity, cancellationToken);
    }

    internal Customer GetCustomer(UserIdentity identity)
    {
        return _userDbService.GetCustomerByIdentity(identity);
    }
}