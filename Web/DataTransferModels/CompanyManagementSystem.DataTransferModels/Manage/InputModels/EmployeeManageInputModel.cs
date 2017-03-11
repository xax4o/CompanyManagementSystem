namespace CompanyManagementSystem.DataTransferModels.Manage.InputModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class EmployeeManageInputModel
    {
        [Display(Name = "Add Employees")]
        public IEnumerable<int> EmployeesToAdd { get; set; }

        public IEnumerable<SelectListItem> ListOfFreeEmployees { get; set; }

        [Display(Name = "Remove Employees")]
        public IEnumerable<int> EmployeesToRemove { get; set; }

        public IEnumerable<SelectListItem> ListOfManagedEmployees { get; set; }
    }
}