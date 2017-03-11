namespace CompanyManagementSystem.DataTransferModels.Employees.InputModels
{
    using System.ComponentModel.DataAnnotations;

    using CompanyManagementSystem.Mappings.Contracts;
    using CompanyManagementSystem.Models;

    public class EmployeeInputModel : EmployeeBaseInputModel, IMapTo<Employee>
    {
        [Required]
        public CompanyRoleType? Position { get; set; }
    }
}