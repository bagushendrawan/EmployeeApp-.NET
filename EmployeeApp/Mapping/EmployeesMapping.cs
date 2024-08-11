using System;
using EmployeeApp.Dtos;
using EmployeeApp.Entities;

namespace EmployeeApp.Mapping;

public static class EmployeesMapping
{
    public static EmployeesEntities ToEntity(this CreateEmployeeDTO employee)
    {
        return new EmployeesEntities()
        {
            NIK = employee.NIK,
            Name = employee.Name,
            Gender = employee.Gender,
            Birth = employee.Birth,
            Address = employee.Address,
            Country = employee.Country
        };
    }

    public static EmployeeDTO ToDto(this EmployeesEntities employee)
    {
        return new(
                employee.NIK,
                employee.Name,
                employee.Gender!,
                employee.Birth!,
                employee.Address!,
                employee.Country!
        );
    }

    public static EmployeesEntities ToEntity(this EditEmployeeDTO employee, string NIK)
    {
        return new EmployeesEntities()
        {
            NIK = NIK,
            Name = employee.Name,
            Gender = employee.Gender,
            Birth = employee.Birth,
            Address = employee.Address,
            Country = employee.Country
        };
    }
}
