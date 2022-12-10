using LastExamBackEndProject.Common.Exceptions;
using LastExamBackEndProject.Domain;

namespace LastExamBackEndProject.Infrastructure.Models.DomainEntities;

public class PlaceEntity : Place
{
    public PlaceEntity(int id, string title, string description, string photoLink, List<string> photos, List<Review> reviews)
        : base(id, title, description, photoLink, photos, reviews)
    {
    }
}
