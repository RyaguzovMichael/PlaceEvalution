namespace LastExamBackEndProject.Infrastructure.Models;

public class UserDbModel : BaseRepositoryEntity
{
    public string Login { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
}