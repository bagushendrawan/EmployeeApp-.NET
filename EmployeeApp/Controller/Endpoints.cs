using System.Diagnostics;
using EmployeeApp.Data;
using EmployeeApp.Dtos;
using EmployeeApp.Entities;
using EmployeeApp.Mapping;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApp.Endpoints;

public static class Endpoints
{
    const string EndpointName = "GetEmployee";
    const string EndpointFindName = "FindEmployee";

    public static RouteGroupBuilder MapEmployeesEndpoints(this WebApplication app)
    {

        var group = app.MapGroup("employees");
        //GET /employees
        group.MapGet("", async (EmployeesDataContext dbContext) => await dbContext.Employees.ToListAsync());

        //GET /employees/{nik}
        group.MapGet("/{nik}", async (string nik, EmployeesDataContext dbContext) =>
        {
            EmployeesEntities? employee = await dbContext.Employees.FindAsync(nik);

            return employee is null ? Results.NotFound() : Results.Ok(employee);
        }).WithName(EndpointName);

        //GET /employees/find
        group.MapGet("find", async (string nik, string name, EmployeesDataContext dbContext) =>
        {
            List<EmployeesEntities> employees = await dbContext.Employees
                .Where(e => e.NIK.Contains(nik) && e.Name.ToLower().Contains(name.ToLower()))
                .ToListAsync();

            return employees.Count == 0 ? Results.NoContent() : Results.Ok(employees);
        }).WithName(EndpointFindName);

        //POST /employee
        group.MapPost("", async (CreateEmployeeDTO newEmployee, EmployeesDataContext dbContext) =>
        {
            EmployeesEntities employees = newEmployee.ToEntity();
            if (DateTime.TryParse(newEmployee.Birth, out DateTime birthDateTime))
            {
                DateOnly birthDateOnly = DateOnly.FromDateTime(birthDateTime);
                DateOnly now = DateOnly.FromDateTime(DateTime.Now);
                if (birthDateOnly > now)
                {
                    return Results.BadRequest("Please input correct birthdate.");
                }
            }
            else
            {
                return Results.BadRequest("Invalid date format.");
            }

            await dbContext.Employees.AddAsync(employees);
            try
            {
                await dbContext.SaveChangesAsync();
                return Results.CreatedAtRoute(EndpointName, new { nik = employees.NIK }, employees.ToDto());
            }
            catch (Exception error)
            {
                return Results.Problem("Error: " + error.Message);
            }


        }).WithParameterValidation();

        //PUT /employees/{nik}
        group.MapPut("/{nik}", async (string nik, EditEmployeeDTO editedEmployee, EmployeesDataContext dbContext) =>
            {
                var existingEmployee = await dbContext.Employees.FindAsync(nik);
                if (DateTime.TryParse(editedEmployee.Birth, out DateTime birthDateTime))
                {
                    DateOnly birthDateOnly = DateOnly.FromDateTime(birthDateTime);
                    DateOnly now = DateOnly.FromDateTime(DateTime.Now);
                    if (birthDateOnly > now)
                    {
                        return Results.BadRequest("Please input correct birthdate.");
                    }
                }
                else
                {
                    return Results.BadRequest("Invalid date format.");
                }

                if (existingEmployee is null)
                {
                    return Results.NotFound();
                }

                dbContext.Entry(existingEmployee).CurrentValues.SetValues(editedEmployee.ToEntity(nik));

                try
                {
                    await dbContext.SaveChangesAsync();
                    return Results.NoContent();
                }
                catch (Exception error)
                {
                    return Results.Problem("Error: " + error.Message);
                }

            }).WithParameterValidation();

        //DELETE /employees/{nik}
        group.MapDelete("/{nik}", async (string nik, EmployeesDataContext dbContext) =>
        {
            await dbContext.Employees.Where(employee => employee.NIK == nik).ExecuteDeleteAsync();
            return Results.NoContent();
        });

        return group;
    }
}
