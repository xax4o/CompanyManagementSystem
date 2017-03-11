namespace CompanyManagementSystem.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using CompanyManagementSystem.Data.Repositories.Contracts;
    using CompanyManagementSystem.Models;
    using CompanyManagementSystem.Services.Contracts;

    public class TeamsService : ITeamsService
    {
        private IRepository<Team> teams;
        private IEmployeesService employeesService;
        private IProjectsService projectsService;

        public TeamsService(IRepository<Team> teams, IEmployeesService employeesService, IProjectsService projectsService)
        {
            this.teams = teams;
            this.employeesService = employeesService;
            this.projectsService = projectsService;
        }

        public Team GetTeamById(int id)
        {
            return this.teams.GetById(id);
        }

        public IQueryable<Team> GetTeamsPaged(int page, int size)
        {
            return this.teams
                .All()
                .OrderByDescending(e => e.Id)
                .Skip((page - 1) * size)
                .Take(size);
        }

        public IQueryable<Team> AllTeams()
        {
            return this.teams.All();
        }

        public void AddTeam(Team team, int teamLeadsId)
        {
            var teamLeader = this.employeesService.GetEmployeeById(teamLeadsId);

            if (teamLeader == null)
            {
                return;
            }

            team.Employees.Add(teamLeader);

            this.teams.Add(team);
            this.teams.SaveChanges();
        }

        public void AddTeamMemebers(Team team, IEnumerable<int> employeesIdsToAdd)
        {
            var teamLeader = team.Employees.FirstOrDefault(e => e.Position == CompanyRoleType.TeamLeader);

            if (teamLeader == null || employeesIdsToAdd == null)
            {
                return;
            }

            foreach (var id in employeesIdsToAdd)
            {
                var employeeToAdd = this.employeesService.GetEmployeeById(id);

                team.Employees.Add(employeeToAdd);
                employeeToAdd.Manager = teamLeader;
            }

            this.teams.SaveChanges();
        }

        public void RemoveTeamMemebers(Team team, IEnumerable<int> employeesIdsToRemove)
        {
            var teamLeader = team.Employees.FirstOrDefault(e => e.Position == CompanyRoleType.TeamLeader);

            if (teamLeader == null || employeesIdsToRemove == null)
            {
                return;
            }

            foreach (var id in employeesIdsToRemove)
            {
                var employeeToRemove = this.employeesService.GetEmployeeById(id);

                employeeToRemove.Team = null;
                employeeToRemove.Manager = null;
            }

            this.teams.SaveChanges();
        }

        public void AddProjectToTeam(Team team, Project project)
        {
            if (project == null)
            {
                return;
            }

            var currentProject = team.Projects.FirstOrDefault(p => p.Status == ProjectStatus.WorkingOn);

            if (currentProject != null)
            {
                currentProject.Status = ProjectStatus.Finished;
            }

            team.Projects.Add(project);
            project.Status = ProjectStatus.WorkingOn;

            this.teams.SaveChanges();
        }
    }
}
