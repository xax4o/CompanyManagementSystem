namespace CompanyManagementSystem.Web.ViewModels.Teams.InputModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using CompanyManagementSystem.Models;
    using CompanyManagementSystem.Web.Mappings.Contracts;

    public class TeamInputModel : IMapTo<Team>
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Team Leader")]
        public int ProjectTeamLeadId { get; set; }

        public IEnumerable<SelectListItem> TeamLeads { get; set; }

        [Display(Name = "Team Members")]
        public IEnumerable<int> TeamMembersIds { get; set; }

        public IEnumerable<SelectListItem> TeamMembers { get; set; }
    }
}