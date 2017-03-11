namespace CompanyManagementSystem.DataTransferModels.Employees.InputModels
{
    using System.ComponentModel.DataAnnotations;

    using CompanyManagementSystem.Mappings.Contracts;
    using CompanyManagementSystem.Models;

    public class EmployeeInputModel : EmployeeBaseInputModel, IMapTo<Employee>
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public CompanyRoleType? Position { get; set; }
    }
}