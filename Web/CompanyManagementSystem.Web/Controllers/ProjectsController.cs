namespace CompanyManagementSystem.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using CompanyManagementSystem.DataTransferModels.Projects.InputModels;
    using CompanyManagementSystem.DataTransferModels.Projects.ViewModels;
    using CompanyManagementSystem.Mappings;
    using CompanyManagementSystem.Models;
    using CompanyManagementSystem.Services.Contracts;

    public class ProjectsController : BaseController
    {
        private IProjectsService projectsService;
        private IMappingService mappingService;

        public ProjectsController(IProjectsService projectsService, IMappingService mappingService)
        {
            this.projectsService = projectsService;
            this.mappingService = mappingService;
        }

        [HttpGet]
        public ActionResult Index(int page = 1, int size = 10)
        {
            var projectsCount = this.projectsService.AllProjects().Count();

            var totalPages = (int)Math.Ceiling(projectsCount / (decimal)size);

            var projects = this.projectsService
                .GetProjectsPaged(page, size)
                .To<ProjectViewModel>()
                .ToList();

            var listViewModel = new ProjectsPagedViewModel
            {
                PageSize = size,
                CurrentPage = page,
                TotalPages = totalPages,
                Projects = projects
            };

            return View(listViewModel);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ProjectInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var project = this.mappingService.Map<Project>(model);

            this.projectsService.AddProject(project);

            return RedirectToAction("Index");
        }
    }
}