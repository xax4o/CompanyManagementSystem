namespace CompanyManagementSystem.DataTransferModels.Employees.InputModels
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class EmployeeManageInputModel
    {
        public IEnumerable<int> TeamMembersToAdd { get; set; }

        public IEnumerable<SelectListItem> ListMembersToAdd { get; set; }

        public IEnumerable<int> TeamMembersToRemove { get; set; }

        public IEnumerable<SelectListItem> ListMembersToRemove { get; set; }
    }
}