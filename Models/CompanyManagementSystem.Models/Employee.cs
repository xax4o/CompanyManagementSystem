namespace CompanyManagementSystem.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Employee
    {
        private ICollection<Team> teams;

        public Employee()
        {
            this.teams = new HashSet<Team>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public CompanyRoleType Position { get; set; }

        [Required]
        public decimal Salary { get; set; }

        [MaxLength(100)]
        public string Workplace { get; set; }

        [MaxLength(30)]
        public string Email { get; set; }

        [MaxLength(15)]
        public string Phone { get; set; }

        public int? EmployeeId { get; set; }

        public virtual Employee Manager { get; set; }

        public int? TeamId { get; set; }

        public virtual Team Team { get; set; }
    }
}

