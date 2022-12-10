namespace LastExamBackEndProject.Domain;

public class UserIdentity
{
    public int Id { get; private set; }

    public UserIdentity(int id)
    {
        Id = id;
    }

    public sealed override bool Equals(object? obj)
    {
        if (obj is UserIdentity user)
        {
            return Id == user.Id;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}