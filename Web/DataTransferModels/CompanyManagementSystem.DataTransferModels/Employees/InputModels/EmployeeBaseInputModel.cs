namespace CompanyManagementSystem.DataTransferModels.Employees.InputModels
{
    using System.ComponentModel.DataAnnotations;

    public class EmployeeBaseInputModel
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public decimal Salary { get; set; }

        [MaxLength(100)]
        public string Workplace { get; set; }

        [MaxLength(15)]
        public string Phone { get; set; }
    }
}