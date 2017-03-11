namespace CompanyManagementSystem.DataTransferModels.Employees.ViewModels
{
    using System.Collections.Generic;

    using CompanyManagementSystem.DataTransferModels.Common;

    public class EmployeesPagedViewModel : BasePagedViewModel
    {
        public IEnumerable<EmployeeViewModel> Employees { get; set; }
    }
}