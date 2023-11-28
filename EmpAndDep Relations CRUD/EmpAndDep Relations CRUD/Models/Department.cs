using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace EmpAndDep_Relations_CRUD.Models
{
    public class Department
    {
        [Key]
        public int Department_Id { get; set; }

        [Required(ErrorMessage = "Enter Your Name ")]
        [Display(Name = "Department Name")]
        [Column(TypeName = "Varchar(50)")]
        public string Department_Name { get; set; }

        public ICollection<Employee> employees { get; set; }
    }
}

