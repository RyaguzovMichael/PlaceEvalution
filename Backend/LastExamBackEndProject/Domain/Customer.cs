namespace LastExamBackEndProject.Domain;

public class Customer : UserIdentity
{
    public string Name { get; set; }
    public string Surname { get; set; }

    protected Customer(int id, string name, string surname) : base(id)
    {
        Name = name;
        Surname = surname;
    }
}