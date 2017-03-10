namespace CompanyManagementSystem.Services.Contracts
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using CompanyManagementSystem.Models;

    public interface IProjectsService
    {
        void AddProject(Project projectToAdd);

        Project GetProjectById(int id);

        IQueryable<Project> AllProjects();

        IQueryable<Project> GetProjectsPaged(int page, int size);

        IEnumerable<SelectListItem> SelectListItemGenerator(IEnumerable<Project> projects);
    }
}
