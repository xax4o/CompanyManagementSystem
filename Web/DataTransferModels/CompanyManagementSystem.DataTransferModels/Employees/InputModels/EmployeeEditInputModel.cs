namespace CompanyManagementSystem.DataTransferModels.Employees.InputModels
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using CompanyManagementSystem.Mappings.Contracts;
    using CompanyManagementSystem.Models;

    public class EmployeeEditInputModel : EmployeeBaseInputModel, IMapFrom<Employee>, IMapTo<Employee>
    {
        public string Name { get; set; }

        public int? Position { get; set; }

        public IEnumerable<SelectListItem> AvailablePositions { get; set; }
    }
}