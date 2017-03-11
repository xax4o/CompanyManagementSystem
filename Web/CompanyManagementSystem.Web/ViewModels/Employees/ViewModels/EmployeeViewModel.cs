﻿namespace CompanyManagementSystem.Web.ViewModels.Employees.ViewModels
{
    using AutoMapper;

    using CompanyManagementSystem.Models;
    using CompanyManagementSystem.Web.Mappings.Contracts;

    public class EmployeeViewModel : EmployeeBaseViewModel, IMapFrom<Employee>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string ManagerName { get; set; }

        public string TeamName { get; set; }


        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Employee, EmployeeViewModel>()
                .ForMember(m => m.ManagerName, opt => opt.MapFrom(e => e.Manager.Name))
                .ForMember(m => m.TeamName, opt => opt.MapFrom(e => e.Team.Name));
        }
    }
}
