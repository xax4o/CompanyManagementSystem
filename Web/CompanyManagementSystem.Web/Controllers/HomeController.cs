namespace CompanyManagementSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using CompanyManagementSystem.Data.Repositories.Contracts;
    using CompanyManagementSystem.Models;

    public class HomeController : BaseController
    {
        private IRepository<Team> teams;

        public HomeController(IRepository<Team> teams)
        {
            this.teams = teams;
        }

        public ActionResult Index()
        {
            var result = this.teams.All().FirstOrDefault();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
    }
}