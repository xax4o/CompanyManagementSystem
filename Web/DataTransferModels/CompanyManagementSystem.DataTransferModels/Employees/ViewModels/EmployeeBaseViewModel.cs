namespace CompanyManagementSystem.DataTransferModels.Employees.ViewModels
{
    using CompanyManagementSystem.Mappings.Contracts;
    using CompanyManagementSystem.Models;

    public class EmployeeBaseViewModel : IMapFrom<Employee>
    {
        public string Name { get; set; }

        public CompanyRoleType Position { get; set; }

        public decimal Salary { get; set; }

        public string Email { get; set; }

        public string Workplace { get; set; }

        public string Phone { get; set; }
    }
}