using BaseRepository.Abstractions;
using LastExamBackEndProject.Domain;
using LastExamBackEndProject.Infrastructure.Models.DbModels;

namespace LastExamBackEndProject.Infrastructure.Abstractions;

public interface IUserRepository : IBaseRepository<UserDbModel>
{
    User ConvertDbModelToUser(UserDbModel model);
    Customer ConvertDbModelToCustomer(UserDbModel model);
}
