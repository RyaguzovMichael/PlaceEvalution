namespace PlaceEvalution.API.API.Models.Commands;

public class RegisterCommand
{
    public string Login { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
}