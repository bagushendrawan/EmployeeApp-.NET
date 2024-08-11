using System;
using EmployeeApp.Dtos;
using EmployeeApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApp.Data;

public class EmployeesDataContext(DbContextOptions<EmployeesDataContext> options) : DbContext(options)
{
    public DbSet<EmployeesEntities> Employees => Set<EmployeesEntities>();

    // Seed Data
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmployeesEntities>().HasData(new
        {
            NIK = "1234",
            Name = "John Doe",
            Gender = "Male",
            Birth = "2002-06-07",
            Address = "Jakarta",
            Country = "Indonesia"
        });
    }
}
