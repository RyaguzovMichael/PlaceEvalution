namespace LastExamBackEndProject.Infrastructure.Models;

public class BaseRepositoryEntity
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime LastModifiedDate { get; set; }
}