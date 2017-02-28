namespace CompanyManagementSystem.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Project
    {
        public Project()
        {
            this.Status = ProjectStatus.NotStarted;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public ProjectStatus Status { get; set; }

        public int? TeamId { get; set; }

        public virtual Team Team { get; set; }
    }
}

