namespace LastExamBackEndProject.Domain;

public class Review : ReviewIdentity
{
    public int Rate { get; private set; }
    public string ReviewText { get; private set; }
    public UserIdentity User { get; private set; }
    public DateTime ReviewDate { get; private set; }

    protected Review(int id, int rate, string reviewText, UserIdentity user, DateTime reviewDate) : base(id)
    {
        Rate = rate;
        ReviewText = reviewText;
        User = user;
        ReviewDate = reviewDate;
    }

    public static Review Create(ReviewIdentity identity, int rate, string reviewText, UserIdentity user)
    {
        return new Review(identity.Id, rate, reviewText, user, DateTime.Now);
    }
}
