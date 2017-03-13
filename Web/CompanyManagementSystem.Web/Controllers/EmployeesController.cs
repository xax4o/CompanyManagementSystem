namespace CompanyManagementSystem.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using CompanyManagementSystem.DataTransferModels.Employees.InputModels;
    using CompanyManagementSystem.DataTransferModels.Employees.ViewModels;
    using CompanyManagementSystem.Mappings;
    using CompanyManagementSystem.Models;
    using CompanyManagementSystem.Services.Contracts;
    using CompanyManagementSystem.Web.Common;

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
        public ActionResult Index(int page = Constants.DefaultStartPage, int size = Constants.DefaultPageSize)
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
        public ActionResult Details(int id)
        {
            var employee = this.employeesService.GetEmployeeById(id);

            var employeeDetailsViewModel = this.mappingService.Map<EmployeeDetailsViewModel>(employee);

            employeeDetailsViewModel.Ceo = employeesService
                .GetEmployeesManagersNameAtPosition(CompanyRoleType.CEO, employee);
            employeeDetailsViewModel.DeliveryDirector = employeesService
                .GetEmployeesManagersNameAtPosition(CompanyRoleType.DeliveryDirector, employee);
            employeeDetailsViewModel.ProjectManager = employeesService
                .GetEmployeesManagersNameAtPosition(CompanyRoleType.ProjectManager, employee);
            employeeDetailsViewModel.TeamLeader = employeesService
                .GetEmployeesManagersNameAtPosition(CompanyRoleType.TeamLeader, employee);

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
                ModelState.AddModelError(string.Empty, Constants.CeoExistsMessage);
                return View(model);
            }

            if (!this.employeesService.AllEmployees().Any(e => e.Position == CompanyRoleType.CEO)
                && model.Position != CompanyRoleType.CEO)
            {
                ModelState.AddModelError(string.Empty, Constants.NoCeoMessage);
                return View(model);
            }

            var employee = this.mappingService.Map<Employee>(model);
            this.employeesService.AddEmployee(employee);

            return RedirectToAction(Constants.Index);
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

            //The current employee Position must be bigger than Team Leader because
            //in my enum CEO is equal to zero and trainee is 7
            if (employee.Position > CompanyRoleType.TeamLeader)
            {
                employeeEditInputModel.AvailablePositions = 
                    this.employeesService.SelectListItemGenerator(CompanyRoleType.Senior, CompanyRoleType.Trainee, employee.Position);
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

            return RedirectToAction(Constants.Index);
        }
    }
}