namespace CompanyManagementSystem.Web.ViewModels.Projects.InputModels
{
    using System.ComponentModel.DataAnnotations;

    using CompanyManagementSystem.Models;
    using CompanyManagementSystem.Web.Mappings.Contracts;

    public class ProjectInputModel : IMapTo<Project>
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}