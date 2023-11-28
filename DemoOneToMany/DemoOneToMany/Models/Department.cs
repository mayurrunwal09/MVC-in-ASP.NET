using System.ComponentModel.DataAnnotations;

namespace DemoOneToMany.Models
{
    public class Department
    {
        [Key]
        public int DepId { get; set; }
        public string DepName { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
