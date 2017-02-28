namespace CompanyManagementSystem.Data
{
    using System.Data.Entity;

    using CompanyManagementSystem.Models;

    public class CompanyManagementDbContext : DbContext
    {
        public CompanyManagementDbContext()
            : base("CompanyManagement")
        {

        }

        public virtual IDbSet<Employee> Employees { get; set; }

        public virtual IDbSet<Team> Teams { get; set; }

        public virtual IDbSet<Project> Projects { get; set; }
    }
}
