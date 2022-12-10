namespace LastExamBackEndProject.Domain.Contracts;

public interface IUserDbService
{
    Task<bool> IsLoginUniqueAsync(string login, CancellationToken cancellationToken);
    Task<UserIdentity> GetNewUserIdentityAsync(CancellationToken cancellationToken);
    Task<User> GetUserByLoginAsync(string login, CancellationToken cancellationToken);
    Task<User> GetUserByIdentityAsync(UserIdentity identity, CancellationToken cancellationToken);
    Task<Customer> GetCustomerByIdentityAsync(UserIdentity identity, CancellationToken cancellationToken);
    Task SaveUserDataAsync(User user, CancellationToken cancellationToken);
    Task UpdateCustomerDataAsync(UserIdentity identity, string name, string surname, CancellationToken cancellationToken);
    Customer GetCustomerByIdentity(UserIdentity identity);
}