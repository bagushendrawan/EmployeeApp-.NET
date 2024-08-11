using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeApp.Entities;

public class EmployeesEntities
{
    [Key]
    public required string NIK { get; set; }
    public required string Name { get; set; }
    public string? Gender { get; set; }
    public string? Birth { get; set; }
    public string? Address { get; set; }
    public string? Country { get; set; }
}
