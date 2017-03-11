namespace CompanyManagementSystem.Web.ViewModels.Employees.ViewModels
{
    using AutoMapper;

    using CompanyManagementSystem.Models;
    using CompanyManagementSystem.Web.Mappings.Contracts;

    public class EmployeeDetailsViewModel : EmployeeBaseViewModel, IMapFrom<Employee>, IHaveCustomMappings
    {
        public string TeamName { get; set; }

        public string TeamLead { get; set; }

        public string ProjectManager { get; set; }

        public string DeliveryDirector { get; set; }

        public string Ceo { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Employee, EmployeeDetailsViewModel>()
                .ForMember(m => m.TeamName, opt => opt.MapFrom(e => e.Team.Name));
        }
    }
}