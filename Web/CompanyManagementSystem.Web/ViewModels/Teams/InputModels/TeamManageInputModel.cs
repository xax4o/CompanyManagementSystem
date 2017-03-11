namespace CompanyManagementSystem.Web.ViewModels.Teams.InputModels
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class TeamManageInputModel
    {
        public IEnumerable<int> TeamMembersToAdd { get; set; }

        public IEnumerable<SelectListItem> FreeMembers { get; set; }

        public IEnumerable<int> TeamMembersToRemove { get; set; }

        public IEnumerable<SelectListItem> TeamMembers { get; set; }

        public int? ProjectToAdd { get; set; }

        public IEnumerable<SelectListItem> FreeProjects { get; set; }
    }
}