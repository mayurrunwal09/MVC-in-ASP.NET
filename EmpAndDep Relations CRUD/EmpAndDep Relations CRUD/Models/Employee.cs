using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace EmpAndDep_Relations_CRUD.Models
{
    public class Employee
    {
        [Key]
        public int Employee_Id { get; set; }

        [Required(ErrorMessage = "Enter Your Name ")]
        [Display(Name = "Employee Name")]
        [Column(TypeName = "Varchar(50)")]
        public string Employee_Name { get; set; }

        [Required(ErrorMessage = "Enter Your Address ")]
        [Display(Name = "Employee Address")]
        [Column(TypeName = "Varchar(50)")]
        public string Employee_Addr { get; set; }

        [Required(ErrorMessage = "Enter Your Date Of Birth ")]
        [Display(Name = "Employee DOB")]
        [Column(TypeName = "Varchar(50)")]
        public DateTime Employee_DOB { get; set; }

        [Required(ErrorMessage = "Enter Your Age ")]
        [Display(Name = "Employee Age")]
        [Column(TypeName = "varchar(50)")]
        public int Employee_Age { get; set; }

        [Required(ErrorMessage = "Enter Your Salary ")]
        [Display(Name = "Employee Salary")]
        [Column(TypeName = "decimal(10)")]
        public decimal Employee_Salary { get; set; }

        public Department Department { get; set; }

        public int Department_Id { get; set; }
    
    }
}
