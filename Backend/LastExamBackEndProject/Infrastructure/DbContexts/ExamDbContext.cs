﻿using LastExamBackEndProject.Infrastructure.Models;
using LastExamBackEndProject.Infrastructure.Models.DbMaps;
using Microsoft.EntityFrameworkCore;

namespace LastExamBackEndProject.Infrastructure.DbContexts;

public class ExamDbContext : DbContext
{
    public DbSet<UserDbModel> Users { get; set; }
    
    public ExamDbContext(DbContextOptions<ExamDbContext> options) : base(options) {}
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserDbMap());
    }
}