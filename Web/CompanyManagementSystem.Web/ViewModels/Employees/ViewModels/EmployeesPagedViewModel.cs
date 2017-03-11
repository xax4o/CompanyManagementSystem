namespace CompanyManagementSystem.Web.ViewModels.Employees.ViewModels
{
    using System.Collections.Generic;

    using CompanyManagementSystem.Web.ViewModels.Common;

    public class EmployeesPagedViewModel : BasePagedViewModel
    {
        public IEnumerable<EmployeeViewModel> Employees { get; set; }
    }
}