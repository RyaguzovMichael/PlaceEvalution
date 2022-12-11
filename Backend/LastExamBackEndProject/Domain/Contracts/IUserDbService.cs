namespace LastExamBackEndProject.Domain.Contracts;

public interface IUserDbService
{
    Task<bool> IsLoginUniqueAsync(string login, CancellationToken token);
    Task<UserIdentity> GetNewUserIdentityAsync(CancellationToken token);
    Task<User> GetUserByLoginAsync(string login, CancellationToken token);
    Task<User> GetUserByIdentityAsync(UserIdentity identity, CancellationToken token);
    Task<Customer> GetCustomerByIdentityAsync(UserIdentity identity, CancellationToken token);
    Task SaveUserDataAsync(User user, CancellationToken token);
    Task UpdateCustomerDataAsync(UserIdentity identity, string name, string surname, CancellationToken token);
    Customer GetCustomerByIdentity(UserIdentity identity);
}