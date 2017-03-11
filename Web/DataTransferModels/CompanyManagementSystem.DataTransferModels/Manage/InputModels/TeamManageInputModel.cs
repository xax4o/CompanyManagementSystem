namespace CompanyManagementSystem.DataTransferModels.Manage.InputModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class TeamManageInputModel
    {
        [Display(Name = "Add Employees to Team")]
        public IEnumerable<int> TeamMembersToAdd { get; set; }

        public IEnumerable<SelectListItem> FreeMembers { get; set; }

        [Display(Name = "Remove Employees from Team")]
        public IEnumerable<int> TeamMembersToRemove { get; set; }

        public IEnumerable<SelectListItem> TeamMembers { get; set; }

        [Display(Name = "Add Team Project")]
        public int? ProjectToAdd { get; set; }

        public IEnumerable<SelectListItem> FreeProjects { get; set; }
    }
}