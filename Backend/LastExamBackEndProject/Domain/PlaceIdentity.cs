namespace LastExamBackEndProject.Domain;

public abstract class PlaceIdentity
{
    public int Id { get; set; }

    public PlaceIdentity(int id)
    {
        Id = id;
    }

    public sealed override bool Equals(object? obj)
    {
        if (obj is PlaceIdentity palce)
        {
            return Id == palce.Id;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
