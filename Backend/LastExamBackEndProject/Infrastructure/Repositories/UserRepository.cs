using LastExamBackEndProject.Infrastructure.DbContexts;
using LastExamBackEndProject.Infrastructure.Models;

namespace LastExamBackEndProject.Infrastructure.Repositories;

public class UserRepository : BaseRepository<UserDbModel, ExamDbContext>
{
    public UserRepository(ExamDbContext dbContext) : base(dbContext)
    {
    }

    protected override IQueryable<UserDbModel> FilterByString(IQueryable<UserDbModel> query, string? filterString)
    {
        return filterString is null ? query : query.Where(u => u.Name.Contains(filterString) || u.Surname.Contains(filterString));
    }
}