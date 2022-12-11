using BaseRepository.Abstractions;
using LastExamBackEndProject.Domain;
using LastExamBackEndProject.Infrastructure.Models.DbModels;

namespace LastExamBackEndProject.Infrastructure.Abstractions;

public interface IPlaceRepository : IBaseRepository<PlaceDbModel>
{
    Place ConvertDbModelToPlace(PlaceDbModel model);
}
