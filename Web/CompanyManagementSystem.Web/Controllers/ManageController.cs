﻿namespace CompanyManagementSystem.Web.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using CompanyManagementSystem.DataTransferModels.Manage.InputModels;
    using CompanyManagementSystem.Models;
    using CompanyManagementSystem.Services.Contracts;
    using CompanyManagementSystem.Web.Common;

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
                return View(Constants.NoManager);
            }

            // Getting all employees with a position one step lower.
            // If DeliveryManager(Enum position = 1) get all ProjectManagers(Enum position = 2).
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
                ListOfFreeEmployees = this.employeesService.SelectListItemGenerator(employeesToAdd),
                ListOfManagedEmployees = this.employeesService.SelectListItemGenerator(employeesToRemove)
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

            //Only CEO(enum 0), DeliveryDirector(enum 1), ProjectManager(enum 2) can manage employees.
            if (employee.Position > CompanyRoleType.ProjectManager)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (!this.employeesService.IfEmployeesCanBeRemovedChecker(employee, model.EmployeesToRemove))
            {
                return View(Constants.CannotRemoveEmployee);
            }

            this.employeesService.AddEmployeesToManager(id, model.EmployeesToAdd);
            this.employeesService.RemoveEmployeesFromManager(model.EmployeesToRemove);

            return RedirectToAction(Constants.Index, Constants.Employees);
        }

        [HttpGet]
        public ActionResult Team(int id)
        {
            var team = this.teamsService.GetTeamById(id);

            if (team == null)
            {
                return HttpNotFound();
            }

            //Only Senior(enum 4), Intermediate(enum 5), Junior(enum 6) and Trainee(enum 7) can be added and removed from team.
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

            return RedirectToAction(Constants.Index, Constants.Teams);
        }
    }
}