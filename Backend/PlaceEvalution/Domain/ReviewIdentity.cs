namespace PlaceEvalution.API.Domain;

public abstract class ReviewIdentity
{
    public int Id { get; private set; }

    public ReviewIdentity(int id)
    {
        Id = id;
    }

    public sealed override bool Equals(object? obj)
    {
        if (obj is ReviewIdentity review)
        {
            return Id == review.Id;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
