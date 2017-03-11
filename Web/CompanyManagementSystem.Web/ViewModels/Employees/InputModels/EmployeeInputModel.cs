namespace CompanyManagementSystem.Web.ViewModels.Employees.InputModels
{
    using System.ComponentModel.DataAnnotations;

    using CompanyManagementSystem.Models;
    using CompanyManagementSystem.Web.Mappings.Contracts;

    public class EmployeeInputModel : EmployeeBaseInputModel, IMapTo<Employee>
    {
        [Required]
        public CompanyRoleType? Position { get; set; }
    }
}