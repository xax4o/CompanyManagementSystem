namespace CompanyManagementSystem.Web.ViewModels.Employees.InputModels
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using CompanyManagementSystem.Models;
    using CompanyManagementSystem.Web.Mappings.Contracts;

    public class EmployeeEditInputModel : EmployeeBaseInputModel, IMapFrom<Employee>, IMapTo<Employee>
    {
        public int? Position { get; set; }

        public IEnumerable<SelectListItem> AvailablePositions { get; set; }
    }
}