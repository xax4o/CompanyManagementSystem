namespace CompanyManagementSystem.Services.Contracts
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using CompanyManagementSystem.Models;

    public interface IEmployeesService
    {
        void AddEmployee(Employee employeeToAdd);

        void AddEmployeesToManager(int managerId, IEnumerable<int> idsOfEmployeesToAdd);

        void RemoveEmployeesFromManager(IEnumerable<int> idsOfEmployeesToRemove);

        void UpdateEmployee(int id, string phone, decimal salary, string email, string workplace, int? positionAsInt);

        string GetEmployeesManagersNameAtPosition(CompanyRoleType type, Employee employee);

        Employee GetEmployeeById(int id);

        IQueryable<Employee> AllEmployees();

        IQueryable<Employee> GetEmployeesPaged(int page, int size);

        IEnumerable<SelectListItem> SelectListItemGenerator(CompanyRoleType start, CompanyRoleType end, CompanyRoleType selected);

        IEnumerable<SelectListItem> SelectListItemGenerator(IEnumerable<Employee> employees);
    }
}
