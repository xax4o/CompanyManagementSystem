namespace CompanyManagementSystem.Web.ViewModels.Employees.ViewModels
{
    using CompanyManagementSystem.Models;

    public class EmployeeBaseViewModel
    {
        public string Name { get; set; }

        public CompanyRoleType Position { get; set; }

        public decimal Salary { get; set; }

        public string Workplace { get; set; }

        public string Phone { get; set; }
    }
}