namespace CompanyManagementSystem.DataTransferModels.Projects.ViewModels
{
    using CompanyManagementSystem.Mappings.Contracts;
    using CompanyManagementSystem.Models;

    public class ProjectBaseViewModel : IMapFrom<Project>
    {
        public string Name { get; set; }

        public ProjectStatus Status { get; set; }
    }
}
