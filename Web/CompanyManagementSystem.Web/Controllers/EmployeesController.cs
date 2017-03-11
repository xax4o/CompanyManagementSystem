namespace CompanyManagementSystem.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using CompanyManagementSystem.DataTransferModels.Employees.InputModels;
    using CompanyManagementSystem.DataTransferModels.Employees.ViewModels;
    using CompanyManagementSystem.Mappings;
    using CompanyManagementSystem.Models;
    using CompanyManagementSystem.Services.Contracts;

    public class EmployeesController : BaseController
    {
        private IEmployeesService employeesService;
        private IMappingService mappingService;

        public EmployeesController(IEmployeesService employeesService, IMappingService mappingService)
        {
            this.employeesService = employeesService;
            this.mappingService = mappingService;
        }

        [HttpGet]
        public ActionResult Index(int page = 1, int size = 10)
        {
            var employeesCount = this.employeesService
                .AllEmployees()
                .Count();

            var totalPages = (int)Math.Ceiling(employeesCount / (decimal)size);

            var pagedEmployees = this.employeesService
                .GetEmployeesPaged(page, size)
                .To<EmployeeViewModel>()
                .ToList();

            var pagedEmployeesViewModel = new EmployeesPagedViewModel
            {
                PageSize = size,
                CurrentPage = page,
                TotalPages = totalPages,
                Employees = pagedEmployees
            };

            return View(pagedEmployeesViewModel);
        }

        [HttpGet]
        public ActionResult Employee(int id)
        {
            var employee = this.employeesService.GetEmployeeById(id);

            var employeeDetailsViewModel = this.mappingService.Map<EmployeeDetailsViewModel>(employee);

            employeeDetailsViewModel.Ceo = employeesService.GetEmployeesManagersNameAtPosition(CompanyRoleType.CEO, employee);
            employeeDetailsViewModel.DeliveryDirector = employeesService.GetEmployeesManagersNameAtPosition(CompanyRoleType.DeliveryDirector, employee);
            employeeDetailsViewModel.ProjectManager = employeesService.GetEmployeesManagersNameAtPosition(CompanyRoleType.ProjectManager, employee);
            employeeDetailsViewModel.TeamLead = employeesService.GetEmployeesManagersNameAtPosition(CompanyRoleType.TeamLeader, employee);

            return View(employeeDetailsViewModel);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(EmployeeInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (this.employeesService.AllEmployees().Any(e => e.Position == CompanyRoleType.CEO)
                && model.Position == CompanyRoleType.CEO)
            {
                ModelState.AddModelError(string.Empty, "There's already a Ceo added. You cannot add another one.");
                return View(model);
            }

            if (!this.employeesService.AllEmployees().Any(e => e.Position == CompanyRoleType.CEO)
                && model.Position != CompanyRoleType.CEO)
            {
                ModelState.AddModelError(string.Empty, "There's no Ceo added. You must add Seo first.");
                return View(model);
            }

            var employee = this.mappingService.Map<Employee>(model);
            this.employeesService.AddEmployee(employee);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Manage(int id)
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
        public ActionResult Manage(int id, EmployeeManageInputModel model)
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

            this.employeesService.AddEmployeesToManager(id, model.TeamMembersToAdd);
            this.employeesService.RemoveEmployeesFromManager(model.TeamMembersToRemove);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var employee = this.employeesService.GetEmployeeById(id);

            if (employee == null)
            {
                return HttpNotFound();
            }

            var employeeEditInputModel = this.mappingService.Map<EmployeeEditInputModel>(employee);

            if (employee.Position > CompanyRoleType.TeamLeader)
            {
                employeeEditInputModel.AvailablePositions = this.employeesService.SelectListItemGenerator(CompanyRoleType.Senior, CompanyRoleType.Trainee, employee.Position);
            }

            return View(employeeEditInputModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EmployeeEditInputModel model)
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

            if (employee.Position > CompanyRoleType.TeamLeader)
            {
                this.employeesService.UpdateEmployee(id, model.Phone, model.Salary, model.Email, model.Workplace, model.Position);
            }
            else
            {
                this.employeesService.UpdateEmployee(id, model.Phone, model.Salary, model.Email, model.Workplace, null);
            }

            return RedirectToAction("Index");
        }
    }
}