namespace CompanyManagementSystem.Web.ViewModels.Projects.ViewModels
{
    using System.Collections.Generic;

    using CompanyManagementSystem.Web.ViewModels.Common;

    public class ProjectsPagedViewModel : BasePagedViewModel
    {
        public IEnumerable<ProjectViewModel> Projects { get; set; }
    }
}