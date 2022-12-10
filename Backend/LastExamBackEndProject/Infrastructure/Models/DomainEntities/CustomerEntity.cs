using LastExamBackEndProject.Domain;

namespace LastExamBackEndProject.Infrastructure.Models.DomainEntities;

public class CustomerEntity : Customer
{
    public CustomerEntity(int id, string name, string surname) : base(id, name, surname) {}
}