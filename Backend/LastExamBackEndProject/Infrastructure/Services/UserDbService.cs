using LastExamBackEndProject.Common.Exceptions;
using LastExamBackEndProject.Domain;
using LastExamBackEndProject.Domain.Contracts;
using LastExamBackEndProject.Infrastructure.Models;
using LastExamBackEndProject.Infrastructure.Models.DomainEntities;
using LastExamBackEndProject.Infrastructure.Repositories;
using System.Threading;

namespace LastExamBackEndProject.Infrastructure.Services;

public class UserDbService : IUserDbService
{
    private readonly UserRepository _userRepository;

    public UserDbService(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> IsLoginUniqueAsync(string login, CancellationToken cancellationToken)
    {
        return await _userRepository.GetFirstOrDefaultAsync(u => u.Login == login, cancellationToken) is null;
    }

    public async Task<UserIdentity> GetNewUserIdentityAsync(CancellationToken cancellationToken)
    {
        UserDbModel model = await _userRepository.AddAsync(new UserDbModel(), cancellationToken);
        return new UserIdentity(model.Id);
    }

    public async Task<User> GetUserByLoginAsync(string login, CancellationToken cancellationToken)
    {
        UserDbModel? model = await _userRepository.GetFirstOrDefaultAsync(u => u.Login == login, cancellationToken);
        if (model is null)
        {
            throw new DbException($"User with login {login} not found in DB");
        }
        return new UserEntity(model.Id, model.Login, model.Password);
    }

    public async Task<User> GetUserByIdentityAsync(UserIdentity identity, CancellationToken cancellationToken)
    {
        UserDbModel? model = await _userRepository.GetFirstOrDefaultAsync(u => u.Id == identity.Id, cancellationToken);
        if (model is null)
        {
            throw new DbException($"User with Id {identity.Id} not found in DB");
        }
        return new UserEntity(model.Id, model.Login, model.Password);
    }

    public async Task<Customer> GetCustomerByIdentityAsync(UserIdentity identity, CancellationToken cancellationToken)
    {
        UserDbModel? model = await _userRepository.GetFirstOrDefaultAsync(u => u.Id == identity.Id, cancellationToken);
        if (model is null)
        {
            throw new DbException($"User with Id {identity.Id} not found in DB");
        }
        return new CustomerEntity(model.Id, model.Name, model.Surname);
    }

    public async Task SaveUserDataAsync(User user, CancellationToken cancellationToken)
    {
        UserDbModel? model = await _userRepository.GetFirstOrDefaultAsync(u => u.Id == user.Id, cancellationToken);
        if (model is null)
        {
            throw new DbException($"User with Id {user.Id} not found in DB");
        }
        model.Login = user.Login;
        model.Password = user.Password;
        await _userRepository.UpdateAsync(model, cancellationToken);
    }

    public async Task UpdateCustomerDataAsync(UserIdentity identity, string name, string surname, CancellationToken cancellationToken)
    {
        UserDbModel? model = await _userRepository.GetFirstOrDefaultAsync(u => u.Id == identity.Id, cancellationToken);
        if (model is null)
        {
            throw new DbException($"User with Id {identity.Id} not found in DB");
        }
        model.Name = name;
        model.Surname = surname;
        await _userRepository.UpdateAsync(model, cancellationToken);
    }

    public Customer GetCustomerByIdentity(UserIdentity identity)
    {
        UserDbModel? model = _userRepository.GetFirstOrDefaultAsync(u => u.Id == identity.Id, new CancellationToken()).Result;
        if (model is null)
        {
            throw new DbException($"User with Id {identity.Id} not found in DB");
        }
        return new CustomerEntity(model.Id, model.Name, model.Surname);
    }
}