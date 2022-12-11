using BaseRepository.Abstractions;
using PlaceEvolution.API.Domain;
using PlaceEvolution.API.Infrastructure.Models.DbModels;

namespace PlaceEvolution.API.Infrastructure.Abstractions;

public interface IUserRepository : IBaseRepository<UserDbModel>
{
    User ConvertDbModelToUser(UserDbModel model);
    Customer ConvertDbModelToCustomer(UserDbModel model);
}
