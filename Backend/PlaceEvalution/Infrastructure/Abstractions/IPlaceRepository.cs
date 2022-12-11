using BaseRepository.Abstractions;
using PlaceEvolution.API.Domain;
using PlaceEvolution.API.Infrastructure.Models.DbModels;

namespace PlaceEvolution.API.Infrastructure.Abstractions;

public interface IPlaceRepository : IBaseRepository<PlaceDbModel>
{
    Place ConvertDbModelToPlace(PlaceDbModel model);
}
