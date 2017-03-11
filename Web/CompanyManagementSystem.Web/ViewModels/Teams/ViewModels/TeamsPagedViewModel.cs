namespace CompanyManagementSystem.Web.ViewModels.Teams.ViewModels
{
    using System.Collections.Generic;

    using CompanyManagementSystem.Web.ViewModels.Common;

    public class TeamsPagedViewModel : BasePagedViewModel
    {
        public IEnumerable<TeamViewModel> Teams { get; set; }
    }
}