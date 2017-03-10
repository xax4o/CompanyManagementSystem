namespace CompanyManagementSystem.Services.Contracts
{
    using System.Collections.Generic;
    using System.Linq;

    using CompanyManagementSystem.Models;

    public interface ITeamsService
    {
        void AddTeam(Team team, int teamLeadsId);

        void AddTeamMemebers(Team team, IEnumerable<int> employeesIdsToAdd);

        void AddProjectToTeam(Team team, Project project);

        void RemoveTeamMemebers(Team team, IEnumerable<int> employeesIdsToRemove);

        Team GetTeamById(int id);

        IQueryable<Team> GetTeamsPaged(int page, int size);

        IQueryable<Team> AllTeams();
    }
}
