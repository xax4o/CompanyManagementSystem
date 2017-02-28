namespace CompanyManagementSystem.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Team
    {
        private ICollection<Employee> employees;
        private ICollection<Project> projects;

        public Team()
        {
            this.employees = new HashSet<Employee>();
            this.projects = new HashSet<Project>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Project> Projects
        {
            get
            {
                return this.projects;
            }

            set
            {
                this.projects = value;
            }
        }

        public virtual ICollection<Employee> Employees
        {
            get
            {
                return this.employees;
            }
            set
            {
                this.employees = value;
            }
        }
    }
}

