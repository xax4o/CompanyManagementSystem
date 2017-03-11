namespace CompanyManagementSystem.DataTransferModels.Projects.ViewModels
{
    using System.Collections.Generic;

    using CompanyManagementSystem.DataTransferModels.Common;

    public class ProjectsPagedViewModel : BasePagedViewModel
    {
        public IEnumerable<ProjectViewModel> Projects { get; set; }
    }
}