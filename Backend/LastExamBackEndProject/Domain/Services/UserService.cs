using LastExamBackEndProject.Common.Exceptions;
using LastExamBackEndProject.Domain.Contracts;

namespace LastExamBackEndProject.Domain.Services;

public class UserService
{
    private readonly UserFactory _userFactory;
    private readonly IUserDbService _userDbService;

    public UserService(UserFactory userFactory, IUserDbService userDbService)
    {
        _userFactory = userFactory;
        _userDbService = userDbService;
    }

    public async Task<Customer> LoginUserAsync(string login, string password, CancellationToken cancellationToken)
    {
        User user = await _userFactory.GetUserAsync(login, cancellationToken);
        if (!user.CheckPassword(password)) throw new ValidationDataException("Login password pair not match");
        return await _userFactory.GetCustomerAsync(user, cancellationToken);
    }

    public async Task<Customer> GetCustomerByIdentityAsync(UserIdentity userIdentity, CancellationToken cancellationToken)
    {
        return await _userFactory.GetCustomerAsync(userIdentity, cancellationToken);
    }

    public async Task<Customer> RegisterNewUserAsync(string login, string password, CancellationToken cancellationToken)
    {
        User user = await _userFactory.CreateUserAsync(login, password, cancellationToken);
        await _userDbService.SaveUserDataAsync(user, cancellationToken);
        return await _userFactory.GetCustomerAsync(user, cancellationToken);
    }

    public async Task<Customer> SetCustomerDataAsync(UserIdentity identity, string name, string surname, CancellationToken cancellationToken)
    {
        await _userDbService.UpdateCustomerDataAsync(identity, name, surname, cancellationToken);
        return await _userFactory.GetCustomerAsync(identity, cancellationToken);
    }

    public async Task<User> GetUserByIdentityAsync(UserIdentity userIdentity, CancellationToken cancellationToken)
    {
        return await _userFactory.GetUserAsync(userIdentity, cancellationToken);
    }

    public async Task UpdateUserData(User user, CancellationToken cancellationToken)
    {
        await _userDbService.SaveUserDataAsync(user, cancellationToken);
    }
}