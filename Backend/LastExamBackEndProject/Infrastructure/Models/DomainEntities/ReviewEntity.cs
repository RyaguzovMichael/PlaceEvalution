using LastExamBackEndProject.Domain;

namespace LastExamBackEndProject.Infrastructure.Models.DomainEntities;

public class ReviewEntity : Review
{
    public ReviewEntity(int id, int rate, string reviewText, UserIdentity user, DateTime reviewDate)
        : base(id, rate, reviewText, user, reviewDate)
    {
    }
}
