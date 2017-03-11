namespace CompanyManagementSystem.DataTransferModels.Teams.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using CompanyManagementSystem.Mappings.Contracts;
    using CompanyManagementSystem.Models;

    public class TeamViewModel : IMapFrom<Team>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string TeamLeadsName { get; set; }

        public string ProjectManagersName { get; set; }

        public ICollection<string> TeamMembersNames { get; set; }

        public ICollection<string> ProjectsNames { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Team, TeamViewModel>()
                .ForMember(m => m.TeamLeadsName, opt => opt.MapFrom(t => t.Employees.FirstOrDefault(e => e.Position == CompanyRoleType.TeamLeader).Name))
                .ForMember(m => m.ProjectsNames, opt => opt.MapFrom(t => t.Projects.Select(p => p.Name).ToList()))
                .ForMember(m => m.ProjectManagersName, opt => opt.MapFrom(t => t.Employees.FirstOrDefault(e => e.Position == CompanyRoleType.TeamLeader).Manager.Name))
                .ForMember(m => m.TeamMembersNames, opt => opt.MapFrom(t => t.Employees.Where(e => e.Position != CompanyRoleType.TeamLeader).Select(e => e.Name).ToList()));
        }
    }
}
