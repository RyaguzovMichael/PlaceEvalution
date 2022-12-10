using LastExamBackEndProject.Domain;

namespace LastExamBackEndProject.Infrastructure.Models;

public class PlaceDbModel : BaseRepositoryEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string TitlePhotoLink { get; set; }
    public float Rate { get; set; }
    public List<string>? Photos { get; set; }
    public ICollection<ReviewDbModel>? Reviews { get; set; }
}