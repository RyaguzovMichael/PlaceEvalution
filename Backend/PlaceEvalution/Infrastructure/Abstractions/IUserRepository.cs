using BaseRepository.Abstractions;
using PlaceEvalution.API.Domain;
using PlaceEvalution.API.Infrastructure.Models.DbModels;

namespace PlaceEvalution.API.Infrastructure.Abstractions;

public interface IUserRepository : IBaseRepository<UserDbModel>
{
    User ConvertDbModelToUser(UserDbModel model);
    Customer ConvertDbModelToCustomer(UserDbModel model);
}
