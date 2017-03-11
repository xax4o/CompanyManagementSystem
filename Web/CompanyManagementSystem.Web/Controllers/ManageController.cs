namespace CompanyManagementSystem.Web.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using CompanyManagementSystem.DataTransferModels.Employees.InputModels;
    using CompanyManagementSystem.DataTransferModels.Teams.InputModels;
    using CompanyManagementSystem.Models;
    using CompanyManagementSystem.Services.Contracts;

    public class ManageController : BaseController
    {
        private IEmployeesService employeesService;
        private ITeamsService teamsService;
        private IProjectsService projectsService;

        public ManageController(
            IProjectsService projectsService,
            ITeamsService teamsService,
            IEmployeesService employeesService)
        {
            this.employeesService = employeesService;
            this.teamsService = teamsService;
            this.projectsService = projectsService;
        }

        [HttpGet]
        public ActionResult Employee(int id)
        {
            var employee = this.employeesService.GetEmployeeById(id);

            if (employee == null)
            {
                return HttpNotFound();
            }

            if (employee.Position > CompanyRoleType.ProjectManager)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (employee.Position != CompanyRoleType.CEO && employee.Manager == null)
            {
                return PartialView("Error");
            }

            var employeesToAdd = this.employeesService
                .AllEmployees()
                .Where(e => e.Position == employee.Position + 1 && e.Manager == null)
                .ToList();

            var employeesToRemove = this.employeesService
                .AllEmployees()
                .Where(e => e.EmployeeId == employee.Id)
                .ToList();

            var employeeManageInputModel = new EmployeeManageInputModel
            {
                ListMembersToAdd = this.employeesService.SelectListItemGenerator(employeesToAdd),
                ListMembersToRemove = this.employeesService.SelectListItemGenerator(employeesToRemove)
            };

            return View(employeeManageInputModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Employee(int id, EmployeeManageInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var employee = this.employeesService.GetEmployeeById(id);

            if (employee == null)
            {
                return HttpNotFound();
            }

            if (employee.Position > CompanyRoleType.ProjectManager)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (!this.employeesService.IfEmployeesCanBeRemovedChecker(employee, model.TeamMembersToRemove))
            {
                return PartialView("Error");
            }

            this.employeesService.AddEmployeesToManager(id, model.TeamMembersToAdd);
            this.employeesService.RemoveEmployeesFromManager(model.TeamMembersToRemove);

            return RedirectToAction("Index", "Employees");
        }

        [HttpGet]
        public ActionResult Team(int id)
        {
            var team = this.teamsService.GetTeamById(id);

            if (team == null)
            {
                return HttpNotFound();
            }

            var freeEmployees = this.employeesService
                .AllEmployees()
                .Where(e => e.Position > CompanyRoleType.TeamLeader && e.Team == null)
                .ToList();

            var teamMembers = this.teamsService.GetTeamById(id)
                .Employees
                .Where(e => e.Position != CompanyRoleType.TeamLeader)
                .ToList();

            var freeProjects = this.projectsService
                .AllProjects()
                .Where(p => p.Status == ProjectStatus.NotStarted)
                .ToList();

            var manageTeamViewModel = new TeamManageInputModel
            {
                FreeMembers = this.employeesService.SelectListItemGenerator(freeEmployees),
                TeamMembers = this.employeesService.SelectListItemGenerator(teamMembers),
                FreeProjects = this.projectsService.SelectListItemGenerator(freeProjects)
            };

            return View(manageTeamViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Team(int id, TeamManageInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var team = this.teamsService.GetTeamById(id);

            if (team == null)
            {
                return HttpNotFound();
            }

            this.teamsService.AddTeamMemebers(team, model.TeamMembersToAdd);
            this.teamsService.RemoveTeamMemebers(team, model.TeamMembersToRemove);

            var projectToAdd = this.projectsService.AllProjects().FirstOrDefault(p => p.Id == model.ProjectToAdd);
            this.teamsService.AddProjectToTeam(team, projectToAdd);

            return RedirectToAction("Index", "Teams");
        }
	}
}