namespace PlaceEvalution.API.Domain;

public class UserIdentity
{
    public int Id { get; private set; }
    public UserRoles Role { get; set; }

    public UserIdentity(int id, UserRoles role)
    {
        Id = id;
        Role = role;
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