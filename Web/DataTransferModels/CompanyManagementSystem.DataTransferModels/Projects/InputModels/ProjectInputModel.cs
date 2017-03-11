namespace CompanyManagementSystem.DataTransferModels.Projects.InputModels
{
    using System.ComponentModel.DataAnnotations;

    using CompanyManagementSystem.Mappings.Contracts;
    using CompanyManagementSystem.Models;

    public class ProjectInputModel : IMapTo<Project>
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}