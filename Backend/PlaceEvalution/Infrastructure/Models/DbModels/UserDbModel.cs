using BaseRepository.Models;
using PlaceEvalution.API.Domain;

namespace PlaceEvalution.API.Infrastructure.Models.DbModels;

public class UserDbModel : BaseRepositoryEntity
{
    public string Login { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public UserRoles Role { get; set; }
    public ICollection<ReviewDbModel> Reviews { get; set; }
}