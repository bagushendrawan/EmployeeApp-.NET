using System.ComponentModel.DataAnnotations;

namespace EmployeeApp.Dtos;

public record class CreateEmployeeDTO(
    [Required] string NIK,
    [Required] string Name,
    string Gender,
    string Birth,
    string Address,
    string Country
);