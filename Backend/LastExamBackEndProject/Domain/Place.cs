using LastExamBackEndProject.Common.Exceptions;

namespace LastExamBackEndProject.Domain;

public class Place : PlaceIdentity
{
    public string Title { get; private set; }
    public string Description { get; private set; }
    public string TitlePhotoLink { get; private set; }
    public float Rate { get; private set; }
    public List<string> Photos { get; private set; }
    public List<Review> Reviews { get; private set; }

    private Place(int id, string title, string description, string photoLink) : base(id)
    {
        Title = title;
        Description = description;
        TitlePhotoLink = photoLink;
        Rate = 0.0f;
        Photos = new List<string>();
        Reviews = new List<Review>();
    }

    protected Place(int id, string title, float rate, string description, string photoLink, List<string> photos, List<Review> reviews)
        : this(id, title, description, photoLink)
    {
        Photos = photos;
        Reviews = reviews;
        Rate = rate;
    }

    public static Place Create(PlaceIdentity identity, string title, string description, string photoLink)
    {
        return new Place(identity.Id, title, description, photoLink);
    }

    public void ChangeDescription(string newDescription)
    {
        Description = newDescription;
    }

    public void ChangeTitlePhoto(string newPhotoLink)
    {
        TitlePhotoLink = newPhotoLink;
    }

    public void AddReview(Review review)
    {
        Reviews ??= new List<Review>();
        Reviews.Add(review);
        Rate = (float)Math.Round(Reviews.Sum(review => review.Rate) / (float)Reviews.Count, 1);
    }

    public void AddPhoto(string newPhotoLink)
    {
        Photos ??= new List<string>();
        Photos.Add(newPhotoLink);
    }

    public void DeleteReview(Review review, UserIdentity userIdentity)
    {
        if (userIdentity.Role == UserRoles.Admin)
        {
            if (Reviews is not null)
            {
                Reviews.Remove(review);
                Rate = (float)Math.Round(Reviews.Sum(review => review.Rate) / (float)Reviews.Count, 1);
            }
        }
        else
        {
            throw new UserAccessException($"User with id : {userIdentity.Id}, don't have permissions to delete reviews");
        }
    }

    public void DeletePhoto(string photoLink, UserIdentity userIdentity)
    {
        if (userIdentity.Role == UserRoles.Admin)
        {
            Photos?.Remove(photoLink);
        }
        else
        {
            throw new UserAccessException($"User with id : {userIdentity.Id}, don't have permissions to delete photos");
        }
    }

    public bool CheckByFilterString(string filterString)
    {
        return Title.Contains(filterString) || Description.Contains(filterString);
    }
}
