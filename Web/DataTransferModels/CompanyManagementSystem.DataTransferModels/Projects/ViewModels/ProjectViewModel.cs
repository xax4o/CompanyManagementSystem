namespace CompanyManagementSystem.DataTransferModels.Projects.ViewModels
{
    using System.Linq;

    using AutoMapper;

    using CompanyManagementSystem.Mappings.Contracts;
    using CompanyManagementSystem.Models;

    public class ProjectViewModel : IMapFrom<Project>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public ProjectStatus Status { get; set; }

        public string TeamName { get; set; }

        public string TeamLeadsName { get; set; }

        public string ProjectManagersName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Project, ProjectViewModel>()
                .ForMember(m => m.TeamName, opt => opt.MapFrom(p => p.Team.Name))
                .ForMember(m => m.TeamLeadsName, opt => opt.MapFrom(p => p.Team.Employees.FirstOrDefault(e => e.Position == CompanyRoleType.TeamLeader).Name))
                .ForMember(m => m.ProjectManagersName, opt => opt.MapFrom(p => p.Team.Employees.FirstOrDefault(e => e.Position == CompanyRoleType.TeamLeader).Manager.Name));
        }
    }
}
