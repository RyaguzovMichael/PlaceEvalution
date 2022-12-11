using BaseRepository;
using PlaceEvolution.API.Common.Exceptions;
using PlaceEvolution.API.Domain;
using PlaceEvolution.API.Domain.Contracts;
using PlaceEvolution.API.Infrastructure.Abstractions;
using PlaceEvolution.API.Infrastructure.Models;
using PlaceEvolution.API.Infrastructure.Models.DbModels;

namespace PlaceEvolution.API.Infrastructure.Services;

public class UserDbService : BaseRepository<UserDbModel, DataBaseContext>, IUserRepository, IUserDbService
{
    public UserDbService(DataBaseContext dbContext) : base(dbContext) { }

    public async Task<bool> IsLoginUniqueAsync(string login, CancellationToken token)
    {
        return await GetFirstOrDefaultAsync(u => u.Login == login, token) is null;
    }

    public async Task<UserIdentity> GetNewUserIdentityAsync(CancellationToken token)
    {
        UserDbModel model = await AddAsync(new UserDbModel(), token);
        return ConvertDbModelToUser(model);
    }

    public async Task<User> GetUserByLoginAsync(string login, CancellationToken token)
    {
        UserDbModel? model = await GetFirstOrDefaultAsync(u => u.Login == login, token);
        if (model is null)
        {
            throw new DatabaseException($"User with login {login} not found in DB");
        }
        return ConvertDbModelToUser(model);
    }

    public async Task<User> GetUserByIdentityAsync(UserIdentity identity, CancellationToken token)
    {
        UserDbModel? model = await GetFirstOrDefaultAsync(u => u.Id == identity.Id, token);
        if (model is null)
        {
            throw new DatabaseException($"User with Id {identity.Id} not found in DB");
        }
        return ConvertDbModelToUser(model);
    }

    public async Task<Customer> GetCustomerByIdentityAsync(UserIdentity identity, CancellationToken token)
    {
        UserDbModel? model = await GetFirstOrDefaultAsync(u => u.Id == identity.Id, token);
        if (model is null)
        {
            throw new DatabaseException($"User with Id {identity.Id} not found in DB");
        }
        return ConvertDbModelToCustomer(model);
    }

    public async Task SaveUserDataAsync(User user, CancellationToken token)
    {
        UserDbModel? model = await GetFirstOrDefaultAsync(u => u.Id == user.Id, token);
        if (model is null)
        {
            throw new DatabaseException($"User with Id {user.Id} not found in DB");
        }
        model.Login = user.Login;
        model.Password = user.Password;
        await UpdateAsync(model, token);
    }

    public async Task UpdateCustomerDataAsync(UserIdentity identity, string name, string surname, CancellationToken token)
    {
        UserDbModel? model = await GetFirstOrDefaultAsync(u => u.Id == identity.Id, token);
        if (model is null)
        {
            throw new DatabaseException($"User with Id {identity.Id} not found in DB");
        }
        model.Name = name;
        model.Surname = surname;
        await UpdateAsync(model, token);
    }

    public Customer GetCustomerByIdentity(UserIdentity identity)
    {
        UserDbModel? model = GetFirstOrDefaultAsync(u => u.Id == identity.Id, new CancellationToken()).Result;
        if (model is null)
        {
            throw new DatabaseException($"User with Id {identity.Id} not found in DB");
        }
        return ConvertDbModelToCustomer(model);
    }

    protected override IQueryable<UserDbModel> FilterByString(IQueryable<UserDbModel> query, string? filterString)
    {
        return filterString is null ? query : query.Where(u => u.Login.Contains(filterString)
                                                            || u.Name.Contains(filterString)
                                                            || u.Surname.Contains(filterString));
    }

    public User ConvertDbModelToUser(UserDbModel model)
    {
        return new UserEntity(model.Id, model.Login, model.Password, model.Role);
    }

    public Customer ConvertDbModelToCustomer(UserDbModel model)
    {
        return new CustomerEntity(model.Id, model.Name, model.Surname, model.Role);
    }

    private class CustomerEntity : Customer
    {
        public CustomerEntity(int id, string name, string surname, UserRoles role) : base(id, name, surname, role) { }
    }

    private class UserEntity : User
    {
        public UserEntity(int id, string login, string password, UserRoles role) : base(id, login, password, role) { }
    }
}