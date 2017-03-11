namespace CompanyManagementSystem.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using CompanyManagementSystem.DataTransferModels.Employees.ViewModels;
    using CompanyManagementSystem.DataTransferModels.Projects.ViewModels;
    using CompanyManagementSystem.DataTransferModels.Teams.InputModels;
    using CompanyManagementSystem.DataTransferModels.Teams.ViewModels;
    using CompanyManagementSystem.Mappings;
    using CompanyManagementSystem.Models;
    using CompanyManagementSystem.Services.Contracts;

    public class TeamsController : BaseController
    {
        private IEmployeesService employeesService;
        private IMappingService mappingService;
        private ITeamsService teamsService;

        public TeamsController(
            ITeamsService teamsService,
            IEmployeesService employeesService,
            IMappingService mappingService)
        {
            this.employeesService = employeesService;
            this.mappingService = mappingService;
            this.teamsService = teamsService;
        }

        [HttpGet]
        public ActionResult Index(int page = 1, int size = 10)
        {
            var allTeamsCount = this.teamsService.AllTeams().Count();

            var totalPages = (int)Math.Ceiling(allTeamsCount / (decimal)size);

            var pagedTeams = this.teamsService
                .GetTeamsPaged(page, size)
                .To<TeamViewModel>()
                .ToList();

            var viewModelToReturn = new TeamsPagedViewModel
            {
                PageSize = size,
                CurrentPage = page,
                Teams = pagedTeams,
                TotalPages = totalPages
            };

            return View(viewModelToReturn);
        }

        [HttpGet]
        public ActionResult Team(int id)
        {
            var team = this.teamsService.GetTeamById(id);

            if (team == null)
            {
                return HttpNotFound();
            }

            var teamViewModel = this.mappingService.Map<TeamDetailsViewModel>(team);

            teamViewModel.Employees = team
                .Employees
                .Where(e => e.Position != CompanyRoleType.TeamLeader)
                .AsQueryable()
                .To<EmployeeBaseViewModel>()
                .ToList();

            teamViewModel.Projects = team
                .Projects
                .AsQueryable()
                .To<ProjectBaseViewModel>()
                .ToList();

            return View(teamViewModel);
        }

        [HttpGet]
        public ActionResult Add()
        {
            var freeTeamLeads = this.employeesService
                .AllEmployees()
                .Where(e => e.Position == CompanyRoleType.TeamLeader && e.Team == null && e.Manager != null)
                .ToList();

            var freeTeamMembers = this.employeesService
                .AllEmployees()
                .Where(e => e.Position > CompanyRoleType.TeamLeader && e.Team == null)
                .ToList();

            var teamsViewModel = new TeamInputModel
            {
                TeamLeads = this.employeesService.SelectListItemGenerator(freeTeamLeads),
                TeamMembers = this.employeesService.SelectListItemGenerator(freeTeamMembers)
            };

            return View(teamsViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(TeamInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var teamLeader = this.employeesService.GetEmployeeById(model.ProjectTeamLeadId);

            if (teamLeader == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var team = this.mappingService.Map<Team>(model);

            this.teamsService.AddTeam(team, model.ProjectTeamLeadId);
            this.teamsService.AddTeamMemebers(team, model.TeamMembersIds);

            return RedirectToAction("Index");
        }
    }
}