﻿using LastExamBackEndProject.Domain;

namespace LastExamBackEndProject.Infrastructure.Models.DomainEntities;

public class UserEntity : User
{
    public UserEntity(int id, string login, string password, UserRoles role) : base(id, login, password, role) {}
}