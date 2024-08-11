using System.ComponentModel.DataAnnotations;

namespace EmployeeApp.Dtos;

public record class EditEmployeeDTO(
    [Required] string Name,
    string Gender,
    string Birth,
    string Address,
    string Country
);
