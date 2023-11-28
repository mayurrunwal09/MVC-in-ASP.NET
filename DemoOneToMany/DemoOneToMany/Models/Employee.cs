using System.ComponentModel.DataAnnotations;

namespace DemoOneToMany.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }

        public Department department { get; set; }
        public int DepId { get; set; }
    }
}
