using BaseRepository.Models;

namespace LastExamBackEndProject.Infrastructure.Models.DbModels;

public class ReviewDbModel : BaseRepositoryEntity
{
    public int Rate { get; set; }
    public string ReviewText { get; set; }
    public int UserId { get; set; }
    public UserDbModel User { get; set; }
    public DateTime ReviewDate { get; set; }
    public int PlaceId { get; set; }
    public PlaceDbModel Place { get; set; }
}
