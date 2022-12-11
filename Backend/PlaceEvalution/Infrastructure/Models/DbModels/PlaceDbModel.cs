using BaseRepository.Models;

namespace PlaceEvalution.API.Infrastructure.Models.DbModels;

public class PlaceDbModel : BaseRepositoryEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string TitlePhotoLink { get; set; }
    public float Rate { get; set; }
    public List<string> Photos { get; set; } = new List<string>();
    public ICollection<ReviewDbModel> Reviews { get; set; } = new List<ReviewDbModel>();
}