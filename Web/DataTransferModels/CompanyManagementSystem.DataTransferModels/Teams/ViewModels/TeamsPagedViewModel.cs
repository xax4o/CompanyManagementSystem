namespace CompanyManagementSystem.DataTransferModels.Teams.ViewModels
{
    using System.Collections.Generic;

    using CompanyManagementSystem.DataTransferModels.Common;

    public class TeamsPagedViewModel : BasePagedViewModel
    {
        public IEnumerable<TeamViewModel> Teams { get; set; }
    }
}