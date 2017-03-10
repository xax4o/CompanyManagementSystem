﻿namespace CompanyManagementSystem.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using CompanyManagementSystem.Data.Repositories.Contracts;
    using CompanyManagementSystem.Models;
    using CompanyManagementSystem.Services.Contracts;

    public class EmployeesService : IEmployeesService
    {
        private IRepository<Employee> employees;

        public EmployeesService(IRepository<Employee> employees)
        {
            this.employees = employees;
        }

        public Employee GetEmployeeById(int id)
        {
            return this.employees.GetById(id);
        }

        public void AddEmployeesToManager(int managerId, IEnumerable<int> idsOfEmployeesToAdd)
        {
            var manager = this.employees.GetById(managerId);

            if (idsOfEmployeesToAdd == null || manager == null)
            {
                return;
            }

            foreach (var item in idsOfEmployeesToAdd)
            {
                var currentEmployee = this.employees.GetById(item);

                currentEmployee.Manager = manager;
            }

            this.employees.SaveChanges();
        }

        public void RemoveEmployeesFromManager(IEnumerable<int> idsOfEmployeesToRemove)
        {
            if (idsOfEmployeesToRemove == null)
            {
                return;
            }

            foreach (var item in idsOfEmployeesToRemove)
            {
                var currentEmployee = this.employees.GetById(item);

                currentEmployee.Manager = null;
            }

            this.employees.SaveChanges();
        }

        public void AddEmployee(Employee employeeToAdd)
        {
            if (employeeToAdd == null)
            {
                return;
            }

            this.employees.Add(employeeToAdd);
            this.employees.SaveChanges();
        }

        public void UpdateEmployee(int id, string phone, decimal salary, string workplace)
        {
            var employee = this.GetEmployeeById(id);

            employee.Phone = phone;
            employee.Salary = salary;
            employee.Workplace = workplace;

            this.employees.Update(employee);
            this.employees.SaveChanges();
        }

        public void UpdateEmployee(int id, string phone, decimal salary, string workplace, int? positionAsInt)
        {
            var employee = this.GetEmployeeById(id);

            employee.Position = (CompanyRoleType)positionAsInt;
            employee.Phone = phone;
            employee.Salary = salary;
            employee.Workplace = workplace;

            this.employees.Update(employee);
            this.employees.SaveChanges();
        }

        public string GetEmployeesManagersNameAtPosition(CompanyRoleType type, Employee employee)
        {
            for (int i = (int)type; i < (int)CompanyRoleType.Trainee; i++)
            {
                if (employee.Manager == null)
                {
                    continue;
                }

                if (employee.Manager.Position == type)
                {
                    return employee.Manager.Name;
                }

                employee = employee.Manager;
            }

            return string.Empty;
        }

        public IEnumerable<SelectListItem> SelectListItemGenerator(CompanyRoleType start, CompanyRoleType end, CompanyRoleType selected)
        {
            var listOfItems = new List<SelectListItem>();
            var isSelected = false;

            for (int i = (int)start; i <= (int)end; i++)
            {
                if ((CompanyRoleType)i == selected)
                {
                    isSelected = true;
                }
                else
                {
                    isSelected = false;
                }

                var selectListItem = new SelectListItem
                {
                    Text = ((CompanyRoleType)i).ToString(),
                    Value = i.ToString(),
                    Selected = isSelected
                };

                listOfItems.Add(selectListItem);
            }

            return listOfItems;
        }

        public IEnumerable<SelectListItem> SelectListItemGenerator(IEnumerable<Employee> employees)
        {
            var listOfItems = new List<SelectListItem>();

            foreach (var employee in employees)
            {
                var selectItem = new SelectListItem
                {
                    Text = employee.Name,
                    Value = employee.Id.ToString()
                };

                listOfItems.Add(selectItem);
            }

            return listOfItems;
        }

        public IQueryable<Employee> GetEmployeesPaged(int page, int size)
        {
            return this.employees
                .All()
                .OrderByDescending(e => e.Id)
                .Skip((page - 1) * size)
                .Take(size);
        }

        public IQueryable<Employee> AllEmployees()
        {
            return this.employees.All();
        }
    }
}
