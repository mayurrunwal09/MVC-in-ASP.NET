using System.ComponentModel.DataAnnotations;

namespace OneToMany1.Models
{
    public class UpdateDepartment
    {
        [Key]
        public int DeptId { get; set; }
        public string DeptName { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
