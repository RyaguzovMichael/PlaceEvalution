using BaseRepository.Abstractions;
using PlaceEvalution.API.Domain;
using PlaceEvalution.API.Infrastructure.Models.DbModels;

namespace PlaceEvalution.API.Infrastructure.Abstractions;

public interface IPlaceRepository : IBaseRepository<PlaceDbModel>
{
    Place ConvertDbModelToPlace(PlaceDbModel model);
}
