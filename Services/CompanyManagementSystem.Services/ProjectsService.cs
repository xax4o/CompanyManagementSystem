namespace CompanyManagementSystem.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using CompanyManagementSystem.Data.Repositories.Contracts;
    using CompanyManagementSystem.Models;
    using CompanyManagementSystem.Services.Contracts;

    public class ProjectsService : IProjectsService
    {
        private IRepository<Project> projects;

        public ProjectsService(IRepository<Project> projects)
        {
            this.projects = projects;
        }

        public IQueryable<Project> AllProjects()
        {
            return this.projects.All();
        }

        public Project GetProjectById(int id)
        {
            return this.projects.GetById(id);
        }

        public void AddProject(Project projectToAdd)
        {
            if (projectToAdd == null)
            {
                return;
            }

            this.projects.Add(projectToAdd);
            this.projects.SaveChanges();
        }

        public IQueryable<Project> GetProjectsPaged(int page, int size)
        {
            return this.projects
                .All()
                .OrderByDescending(e => e.Id)
                .Skip((page - 1) * size)
                .Take(size);
        }

        public IEnumerable<SelectListItem> SelectListItemGenerator(IEnumerable<Project> projects)
        {
            var listOfItems = new List<SelectListItem>();

            foreach (var project in projects)
            {
                var selectItem = new SelectListItem
                {
                    Text = project.Name,
                    Value = project.Id.ToString()
                };

                listOfItems.Add(selectItem);
            }

            return listOfItems;
        }
    }
}
