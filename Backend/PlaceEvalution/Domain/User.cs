using PlaceEvolution.API.Common.Exceptions;
using PlaceEvolution.API.Common.Extensions;

namespace PlaceEvolution.API.Domain;

public class User : UserIdentity
{
    public string Login { get; private set; }
    public string Password { get; private set; }

    protected User(int id, string login, string password, UserRoles role) : base(id, role)
    {
        Login = login;
        Password = password;
    }

    public static User Create(UserIdentity identity, string login, string password)
    {
        if (string.IsNullOrWhiteSpace(login))
        {
            throw new ValidationDataException("Login can't be empty");
        }

        if (string.IsNullOrWhiteSpace(password))
        {
            throw new ValidationDataException("Password can't be empty");
        }

        return new User(identity.Id, login, password.Hash(), identity.Role);
    }

    public bool CheckPassword(string password)
    {
        return Password.CompareHashedString(password);
    }

    public void ChangeLogin(string newLogin)
    {
        if (string.IsNullOrWhiteSpace(newLogin))
        {
            throw new ValidationDataException("Login can't be empty");
        }

        Login = newLogin;
    }

    public void ChangePassword(string newPassword)
    {
        if (string.IsNullOrWhiteSpace(newPassword))
        {
            throw new ValidationDataException("Password can't be empty");
        }

        Password = newPassword.Hash();
    }
}