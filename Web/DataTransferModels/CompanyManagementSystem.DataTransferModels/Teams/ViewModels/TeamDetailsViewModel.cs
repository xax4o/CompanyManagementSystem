namespace CompanyManagementSystem.DataTransferModels.Teams.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using CompanyManagementSystem.DataTransferModels.Employees.ViewModels;
    using CompanyManagementSystem.DataTransferModels.Projects.ViewModels;
    using CompanyManagementSystem.Mappings.Contracts;
    using CompanyManagementSystem.Models;

    public class TeamDetailsViewModel : IMapFrom<Team>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public string TeamLeadsName { get; set; }

        public ICollection<EmployeeBaseViewModel> Employees { get; set; }

        public ICollection<ProjectBaseViewModel> Projects { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Team, TeamDetailsViewModel>()
                .ForMember(m => m.TeamLeadsName, opt => opt.MapFrom(t => t.Employees.FirstOrDefault(e => e.Position == CompanyRoleType.TeamLeader).Name));
        }
    }
}
